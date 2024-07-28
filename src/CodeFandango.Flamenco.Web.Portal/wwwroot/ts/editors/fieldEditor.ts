import { ajax } from '../ajax';
import Reef from 'reefjs';
import { Options } from 'reefjs';
import { helper } from "../helpers";
import { editableFormModel } from "../editableFormModel";

export class fieldEditor {

    getUri;
    setUri;

    model;

    constructor(getUri, setUri) {
        this.getUri = getUri;
        this.setUri = setUri;
        this.refresh();
    }

    refresh() {
        ajax.get(this.getUri).then((x: any)=>{
            this.model = new editableFormModel(x.model.fields, x.model.values);
            this.render();
        });
    }

    render() {

        var html = ``;

        if (!this.model) {
            const reefOptions: Options = { template: '' } 
            const reef = new Reef('#editor', reefOptions);
            reef.render();            
            return
        }

        var groups = this.model.getFieldGroups();
        
        groups.sort(helper.orderBy('order')).forEach(x=>{

            html += `
                <div class="card mb-3">
                    <div class="card-header"><b>${x}</b></div>
                    <div class="card-body"><div class="row">`;
                        
            this.model.getFields().filter(y=>y.group == x).forEach(y=>{
                html += `<div class="col">
                <div class="form-group mb-3">
                    <label><b>${y.name}</b></label>
                    ${y.description ? `<small class="form-text text-muted">${y.description}</small>` : ''}
                    <input type="text" class="form-control" data-code="${y.code}" value="${this.model.getFieldValue(y.code) || ''}" />
                </div></div>`;
            });

            html += `</div></div></div>`;

        });

        const reefOptions: Options = { template: html } 
        const reef = new Reef('#editor', reefOptions);
        reef.render();            
        this.model.bind('#editor');

        this.renderSaveButton();

    }

    renderSaveButton() {

        // The save button should be in a div which is added directly to the body element.

        var html = `
                <button class="btn btn-primary" id="save">Save</button>
                `;

        const reefOptions: Options = { template: html } 
        const reef = new Reef('#buttonbar', reefOptions);
        reef.render();            
        this.bind();

    }

    save = () => {
        if (!this.model.isValid()) {
            alert('Please fill in all required fields.');
            return;
        }
        ajax.post(this.setUri, this.model.getDictionary()).then((x: any)=>{
            if (x.successful) {
                alert('Saved successfully.');
                this.refresh();
            } else {
                alert('An error occurred.');
            }
        });
    }

    bind() {
        document.getElementById('save')?.addEventListener('click', this.save);
    }

}