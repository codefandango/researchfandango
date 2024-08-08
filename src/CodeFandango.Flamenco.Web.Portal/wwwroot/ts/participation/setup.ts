import { ajax } from "../ajax";
// @ts-expect-error
import { render } from 'reefjs';
import { objectEditorSidebar } from "objectEditor";
import { editableFormModel } from "editableFormModel";

interface Success<T> {
    successful: boolean;
    model: T;
}

interface NamedEntity {
    id: number;
    name: string;
    description: string;
}

enum ParticipantFieldType
{
    String = 1,
    Number = 2,
    Boolean = 4,
    Date = 8,
    Time = 16,
    DateTime = 32,
    EmailAddress = 64,
    SurveySelector = 128,
    PhoneNumber = 256,
    PersonallyIdentifiableInformation = 512,
    ValueFromSet = 1024,
    SurveyLevel = 2048,
}

interface ParticipantFieldDefinitionModel extends NamedEntity {
    type: ParticipantFieldType,
    isRequired: boolean,
    order: number,
    group: string,
    showInList: boolean;
    studyId?: number;
    surveyId?: number;
}

interface CustomerModel extends NamedEntity {
}

interface ParticipationSetup {
    participantFieldEditDefinition: editableFormModel;
    participationFieldDefinitions: ParticipantFieldDefinitionModel[];
    permittedCustomers: CustomerModel[];
}

export class participationManager {

    private type: string = 'study';
    private id = 0;
    private setup: ParticipationSetup;
    private sidebar: objectEditorSidebar;

    constructor(type: string, id: number) {
        this.id = id;
        this.type = type;
        this.loadParticipationSetup();
    }

    loadParticipationSetup() {
        ajax.get(`/api/participation/setup/${this.type}/${this.id}`).then((data: Success<ParticipationSetup>) => {
            if (data.successful) {
                this.setup = data.model;
                this.sidebar = new objectEditorSidebar({
                    definition: data.model.participantFieldEditDefinition,
                    typeCode: 'participantField',
                    get: this.get,
                    save: this.save,
                    onClose: () => {}
                });
                this.render();
            }
        }).catch((error) => {
            console.log(error);
        });
    }

    get = (id: number) => {

    }

    save = (model: any) => {
        if (this.type == 'study') {
            model['studyId'] = this.id;
        }
        else {
            model['siteId'] = this.id;
        }

        ajax.post(`/api/participation/field`, model).then((data: Success<any>) => {
            if (data.successful) {
                this.loadParticipationSetup();
            }
        }).catch((error) => {
            console.log(error);
        });
    }

    render() {

        var html = `<div class="row">`;
        html += `<div class="col">`;

        html += '<h5>Participant Fields</h5>';

        // Here we render a table of field definitions from this.setup.ParticipationFieldDefinitions
        html += `<table class="table table-bordered">`;
        html += `<thead>`;
        html += `<tr>`;
        html += `<th>Name</th>`;
        html += `<th>Description</th>`;
        html += `<th>Type</th>`;
        html += `</tr>`;
        html += `</thead>`;
        html += `<tbody>`;
        this.setup.participationFieldDefinitions.forEach(field=>{
            html += `<tr class="${this.type == 'survey' && field.studyId > 0 ? 'noedit' : ''}">`;
            html += `<td>${field.name}</td>`;
            html += `<td>${field.description}</td>`;
            html += `<td>${field.type}</td>`;
            html += `</tr>`;
        });
        html += `</tbody>`;
        html += `</table>`;

        html += '</div>'; // col

        if (this.type == 'study') {

            html += `<div class="col-4">`;
            html += `<h5>Permitted Customers</h5>`;
            html += '</div>';

        }

        html += '</div>'; // row

        render('#editor', html);
        this.bind();

    }

    bind = () => {
        document.querySelectorAll('.add-field').forEach((element) => {
            element.removeEventListener('click', this.sidebar.newObject);
            element.addEventListener('click', this.sidebar.newObject);
        });
    }

}

