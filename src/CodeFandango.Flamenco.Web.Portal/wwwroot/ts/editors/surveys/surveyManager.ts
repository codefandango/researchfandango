import Reef from 'reefjs';
import { Options } from 'reefjs';
import { ajax } from '../../ajax';

export class surveyManager {

    surveys;

    constructor() {

    }

    refresh() {
        ajax.get('/api/admin/surveys').then((x: any)=>{
            this.surveys = x.model;
            this.render();
        });
    }

    render() {

        var html = ``;

        if (!this.surveys) {
            const reefOptions: Options = { template: '' } 
            const reef = new Reef('#surveys', reefOptions);
            reef.render();            
            return
        }

        const reefOptions: Options = { template: html } 
        const reef = new Reef('#editor', reefOptions);
        reef.render();            
    }

}