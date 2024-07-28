import { helper } from 'helpers.js';
import { ajax } from './ajax.js';
import { objectEditorConfiguration, editableFieldDefinition, editableDataType, objectEditorSidebarConfiguration } from './objectEditorTypes.js';
import { sidebar } from './sidebar.js';
// @ts-expect-error
import { render } from 'reefjs';

class objectEditorSidebar extends sidebar {

    config: objectEditorSidebarConfiguration; 
    item: any;

    constructor(config: objectEditorSidebarConfiguration) {
        super();
        this.config = config;
    }

    load = (id: number) => {
        this.config.get(id).then((x: any) => {
            this.item = x.model;
            this.show();
        });
    }

    newObject = () => {
        this.item = {};
        this.show();
    }

}

export class objectEditor {

    config: objectEditorConfiguration; // Add the type annotation for 'config'

    list: any = [];
    sidebar: objectEditorSidebar;
    definition: any = [];

    constructor(config: objectEditorConfiguration) {
        this.config = config;
        if (!config.typeCode) throw new Error('config.typeCode is required');
        if (!config.listUrl) throw new Error('config.listUrl is required');
        if (!config.saveUrl) throw new Error('config.saveUrl is required');
        if (!config.deleteUrl) throw new Error('config.deleteUrl is required');
        if (!config.definitionUrl) throw new Error('config.definitionUrl is required');
        if (!config.getUrl) throw new Error('config.getUrl is required');

        var initialTasks = [];
        initialTasks.push(this.getDefinition());
        initialTasks.push(this.getList());

        Promise.all(initialTasks).then(() => {

            this.sidebar = new objectEditorSidebar({
                typeCode: config.typeCode,
                get: this.get
            });

        });

    }

    get = (id: any): Promise<any> => {
        return ajax.get(this.config.getUrl.replace('{id}', id));
    }

    getDefinition(): Promise<any> {
        return ajax.get(this.config.definitionUrl).then((x: any) => {
            this.definition = x.model;
        });
    }

    getList(): Promise<any> {
        return ajax.get(this.config.listUrl).then((x: any) => {
            this.list = x.model;
            this.render();
        });
    }

    render() {

        if (!this.list && this.list.length == 0) return;
        if (!this.definition && this.definition.length == 0) return;
        
        var html = ``;
        html += `<table class="table table-hover">
        <thead>
            <tr>
                ${this.definition.sort(x=>helper.orderBy('order')).map((x: any) => `<th>${x.name}</th>`).join('')}
            </tr>
        </thead>
        <tbody>`;

        this.list.forEach((x: any) => {
            html += `<tr data-object-id="${x.id}">
                ${this.definition.sort(x=>helper.orderBy('order')).map((y: any) => `<td>${x[y.code] ?? ''}</td>`).join('')}
            </tr>`;
        });

        html += `</tbody></table>`;

        render('#editor', html);
        //this.sidebar.render('#editor', html);
        this.bind();

    }

    bind() {

        document.querySelectorAll('#editor tbody tr').forEach((x: any) => {
            x.removeEventListener('click', this.editObject);
            x.addEventListener('click', this.editObject);
        });


    }

    editObject = (e: any) => {        
        var id = e.currentTarget.getAttribute('data-object-id');
        this.sidebar.load(parseInt(id));
        this.sidebar
    }

}