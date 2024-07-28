export class editableFormModel {

    fields;
    values;

    constructor(fields, values) {
        this.fields = fields;
        this.values = values;
    }

    getFields() {
        return this.fields;
    }

    getFieldValue(fieldCode) {
        return this.values.find(x=>x.fieldCode == fieldCode)?.value;
    }

    setFieldValue(fieldCode, value) {
        var existingValue = this.values.find(x=>x.fieldCode == fieldCode);
        if (!existingValue) {
            this.values.push({fieldCode: fieldCode, value: value});
            return;
        } else {
            existingValue.value = value;
        }
    }

    getFieldGroups() {
        return this.fields.map(x=>x.group).filter((v, i, a) => a.indexOf(v) === i);
    }

    updateModel = (e) => {
        var code = e.target.getAttribute('data-code');
        if (!code) return;
        this.setFieldValue(code, e.target.value);
    }

    bind(selector) {
        document.querySelectorAll(`${selector} input`).forEach(x=>{
            x.removeEventListener('input', this.updateModel);
            x.addEventListener('input', this.updateModel);
        })
    }

    isValid() {
        return this.fields.filter(x=>x.required).every(x=>this.getFieldValue(x.code));
    }

    getDictionary() {
        return this.values.reduce((a, x)=>{
            a[x.fieldCode] = x.value;
            return a;
        }, {});
    }

}