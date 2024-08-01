export interface objectEditorConfiguration {
    typeCode: string,
    listUrl: string;
    saveUrl: string;
    deleteUrl: string;
    definitionUrl: string;
    getUrl: string;
}
export interface sidebarConfiguration {
    onClose: Function;
}
export interface objectEditorSidebarConfiguration extends sidebarConfiguration {
    typeCode: string;
    definition: any;
    get: Function;
    save: Function;
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
    Color,
    Reference
}
export interface sidebarButtonCollection {
    [key: string]: sidebarButtonDefinition;
}
export interface sidebarButtonDefinition {
    text: string;
    icon: string;
    click: Function;
    order: number;
} 
export interface objectValidationError {
    code: string;
    message: string;
}