export interface objectEditorConfiguration {
    typeCode: string,
    listUrl: string;
    saveUrl: string;
    deleteUrl: string;
    definitionUrl: string;
    getUrl: string;
}
export interface objectEditorSidebarConfiguration {
    typeCode: string;
    get: Function;
}
export interface editableFieldDefinition {
    name: string;
    description?: string;
    code: string;
    type: editableDataType;
    isRequired: boolean;
    order: number;
    group?: string;
}
export enum editableDataType {
    String,
    Number,
    Boolean,
    Date,
    Time,
    DateTime,
    Text,
    Image,
    Color
}
