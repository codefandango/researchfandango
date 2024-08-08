import { helper } from 'helpers.js';
import { ajax } from './ajax.js';
import { objectEditorConfiguration, editableFieldDefinition, editableDataType, objectEditorSidebarConfiguration, objectValidationError, objectActionScope } from './objectEditorTypes.js';
import { sidebar } from './sidebar.js';
// @ts-expect-error
import { render } from 'reefjs';
import { on } from 'events';

export class objectEditorSidebar extends sidebar {

    config: objectEditorSidebarConfiguration;
    item: any;
    errors: objectValidationError[] = [];
    codeTimers: any = {};

    constructor(config: objectEditorSidebarConfiguration) {
        super(config);
        this.config = config;
        this.item = [];
        this.errors =[];
        this.addButton('save', {
            text: 'Save',
            icon: 'fas fa-save',
            click: this.save,
            order: 1
        });
    }

    addButtons = () => {
        var orderOffset = 2;
        var currentScope = this.item.id ?? 0 > 0 ? objectActionScope.Edit : objectActionScope.New;

        this.config.definition.actions?.forEach((x: any) => {
            if ((x.scope & currentScope) == 0) return;
            this.addButton(x.code, {
                text: x.name,
                icon: x.icon,
                click: this.actionButtonClick,
                order: x.order + orderOffset
            });
        });
    }

    actionButtonClick = (e) => {
        var action = e.currentTarget.getAttribute('data-button-id');
        var actionDefinition = this.config.definition.actions.find(x => x.code == action);
        if (actionDefinition.action == 'navigate'){
            var url = actionDefinition.path.replace('{id}', this.item.id);
            window.location.href = url;
        }
    }

    load = (id: number) => {
        this.config.get(id).then((x: any) => {
            this.item = x.model;
            this.addButtons();
            this.#setTitle('Edit ' + this.config.typeCode);
            this.show();
            this.render();
        });
    }

    validate = () => {
        var item = this.item;
        var errors: objectValidationError[] = [];
        return new Promise((resolve, reject) => {
            this.config.definition.fields.forEach((x: any) => {
                if (x.isRequired && !item[x.code]) {
                    errors.push({ code: x.code, message: x.name + ' is required' });
                }
            });
            if (errors.length > 0) {
                reject(errors);
            } else {
                resolve(true);
            }
        });
    }

    save = () => {
        this.validate().then(validation=>{
            this.errors = [];
            this.config.save(this.item).then((x: any) => {
                this.close(true);
            });
        }).catch((errors: objectValidationError[]) => {
            this.errors = errors;
            this.render();
        });
    }

    newObject = () => {
        this.item = {};
        this.render();
        this.addButtons();
        this.#setTitle('New ' + this.config.definition?.name || this.config.typeCode);
        this.show();
    }

    getObject = () => {
        var item = {};
        this.config.definition.fields.forEach((x: any) => {

            if (x.type == editableDataType.Boolean) {
                var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
                item[x.code] = field.checked;
            }

            if (x.type == editableDataType.Number) {
                var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
                item[x.code] = parseFloat(field.value);
            }

            if (x.type == editableDataType.String) {
                var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
                item[x.code] = field.value;
            }

            if (x.type == editableDataType.Text) {
                var field: any = this.element.querySelector(`textarea[data-code="${x.code}"]`);
                item[x.code] = field.value;
            }

            if (x.type == editableDataType.Date) {
                var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
                item[x.code] = field.value;
            }

            if (x.type == editableDataType.DateTime) {
                var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
                item[x.code] = field.value;
            }

            if (x.type == editableDataType.Time) {
                var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
                item[x.code] = field.value;
            }

            // if (x.type == editableDataType.Image) {
            //     var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
            //     item[x.code] = field.value;
            // }

            // if (x.type == editableDataType.Color) {
            //     var field: any = this.element.querySelector(`input[data-code="${x.code}"]`);
            //     item[x.code] = field.value;
            // }

            if (x.type == editableDataType.Reference) {
                var field: any = this.element.querySelector(`select[data-code="${x.code}"]`);
                item[x.code] = field.value;
            }

        });

        return item;
    }

    #setTitle = (title) => {
        var titleElement: any = this.element.querySelector('h5.offcanvas-title');
        titleElement.innerText = title;
    }

    render(): string {

        var html = super.render();

        if (!this.item) {
            render(this.bodyElement, html);
            return
        }

        var getReferenceControl = (y: any) => {
            return `<select class="form-control" data-code="${y.code}">
                ${this.config.definition.references[y.objectReference].map((x: any) => `<option value="${x.id}" ${this.item[y.code] == x.id ? 'selected' : ''}>${x.name}</option>`).join('')}
            </select>`;
        }

        var groups = this.config.definition.fields.map(x => x.group).filter((value, index, self) => self.indexOf(value) === index);
 
        groups.sort(helper.orderBy('order')).forEach((x: any) => {

            html += `<div class="h3">${x}</div>`;

            this.config.definition.fields.filter(y => y.group == x && y.type != editableDataType.Code).forEach((y: any) => {
                var error = this.errors?.find(z => z.code == y.code);
                var codeField = this.config.definition.fields.find(z => z.type == editableDataType.Code && z.fieldReference == y.code);
                html += `<div class="row mb-3"><div class="col">
                <div class="form-group mb-3">
                    <label class="d-flex justify-content-between">
                        <b>${y.name}</b>
                        ${codeField ? `<span class="form-code-field" data-for="${y.code}"><i class="fas fa-key"></i> ${this.item[codeField.code] ?? ''}</span>` : ''}
                    </label>
                    ${y.type == editableDataType.Text ? `<textarea class="form-control" placeholder="${y.description}" data-code="${y.code}">${this.item[y.code] || ''}</textarea>` : ''}
                    ${y.type == editableDataType.String ? `<input type="text" class="form-control" placeholder="${y.description}" data-code="${y.code}" @value="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.Number ? `<input type="number" class="form-control" placeholder="${y.description}" data-code="${y.code}" @value="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.Boolean ? `<input type="checkbox" class="form-control" data-code="${y.code}" @checked="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.Date ? `<input type="date" class="form-control" placeholder="${y.description}" data-code="${y.code}" @value="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.Time ? `<input type="time" class="form-control" placeholder="${y.description}" data-code="${y.code}" @value="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.DateTime ? `<input type="datetime-local" placeholder="${y.description}" class="form-control" data-code="${y.code}" @value="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.Image ? `<input type="text" class="form-control" placeholder="${y.description}" data-code="${y.code}" @value="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.Color ? `<input type="color" class="form-control" placeholder="${y.description}" data-code="${y.code}" @value="${this.item[y.code] || ''}" />` : ''}
                    ${y.type == editableDataType.Reference ? getReferenceControl(y) : ''}
                    ${error ? `<small class="text-danger ps-3">${error.message}</small>` : ''}
                </div></div></div></div>`;
            });

            html += ``;

        });

        render(this.bodyElement, html);
        this.bind();

    }

    bind() {
        super.bind();

        this.bodyElement.querySelectorAll('input, select, textarea').forEach((x: any) => {
            x.removeEventListener('input', this.inputChange);
            x.addEventListener('input', this.inputChange);
        });

    }

    inputChange = (e) => {
        var field = e.currentTarget.getAttribute('data-code');
        if (e.currentTarget.getAttribute('type') == 'checkbox') {
            this.item[field] = e.currentTarget.checked;
            return;
        }
        if (e.currentTarget.getAttribute('type') == 'number') {
            this.item[field] = parseFloat(e.currentTarget.value);
            return;
        }
        if (e.currentTarget.tagName == 'SELECT') {
            this.item[field] = e.currentTarget.value;
            return
        }
        this.item[field] = e.currentTarget.value;

        // Is there a code field that needs to be updated?
        var codeField = this.config.definition.fields.find(x => x.type == editableDataType.Code && x.fieldReference == field);
        if (codeField) {
            if (this.codeTimers[field]) clearTimeout(this.codeTimers[field]);
            this.codeTimers[field] = setTimeout(() => this.updateCodeField(field, codeField), 500);
        }

    }

    updateCodeField = (field: any, codeField: editableFieldDefinition) =>  {
        ajax.get('/api/code/' + codeField.uniqueScope + '?value=' + this.item[field]).then((x: any) => {
            this.item[codeField.code] = x.model;
            this.render();
        }).catch((error) => {
            console.log(error);
        });
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

        this.getDefinition()
            .then(defModel => {
                this.definition = defModel.model;
                return this.getList();
            })
            .then(listModel => {

                this.list = listModel.model;

                this.sidebar = new objectEditorSidebar({
                    typeCode: config.typeCode,
                    definition: this.definition,
                    get: this.get,
                    save: this.save,
                    onClose: () => this.refresh()
                });

                this.render();

            });

    }

    refresh = () => {
        this.getList().then(x => {
            this.list = x.model;
            this.render();
        });
    }

    save = (item: any): Promise<any> => {
        return ajax.post(this.config.saveUrl, item);
    }

    get = (id: any): Promise<any> => {
        return ajax.get(this.config.getUrl.replace('{id}', id));
    }

    getDefinition(): Promise<any> {
        return ajax.get(this.config.definitionUrl);
    }

    getList(): Promise<any> {
        return ajax.get(this.config.listUrl);
    }

    render() {

        if (!this.list && this.list.length == 0) return;
        if (!this.definition) return;
        if (!this.definition.fields) return;
        if (this.definition.fields.length == 0) return;

        var html = ``;
        html += `<table class="table table-hover">
        <thead>
            <tr>
                ${this.definition.fields.sort(x => helper.orderBy('order')).map((x: any) => `<th>${x.name}</th>`).join('')}
            </tr>
        </thead>
        <tbody>`;

        this.list.forEach((x: any) => {
            html += `<tr data-object-id="${x.id}">
                ${this.definition.fields.sort(x => helper.orderBy('order')).map((y: any) => `<td>${x[y.code] ?? ''}</td>`).join('')}
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

        document.querySelectorAll('.create-object').forEach((x: any) => {
            x.removeEventListener('click', this.createNewObject);
            x.addEventListener('click', this.createNewObject);
        });

    }

    createNewObject = (e: any) => {
        var id = e.currentTarget.getAttribute('data-object-type');
        if (id != this.config.typeCode) return;
        this.sidebar.newObject();
    }

    editObject = (e: any) => {
        var id = e.currentTarget.getAttribute('data-object-id');
        this.sidebar.load(parseInt(id));
    }

}