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
    definition: editableObjectDefinition | editableFormModel; 
    get: Function;
    save: Function;
}
export interface fieldAnswerModel {
    fieldCode: string;
    value: any;
}
export interface editableObjectDefinition {
    name: string,
    flags: number,
    fields: editableFieldDefinition[],
    references: editableObjectReferenceCollection,
    actions: objectActionDefinition[]
}
export interface objectActionDefinition {
    code: string;
    name: string;
    order: number;
    icon: string;
    action: string;
    path: string;
    scope: objectActionScope;
}
export interface editableObjectReferenceCollection {
    [key: string]: referenceObject[];
}
export interface referenceObject {
    id: number;
    name: string;
}
export interface editableFormModel {
    name?: string;
    fields: editableFieldDefinition[];
    values: fieldAnswerModel[];
    actions?: any;
    references?: any;
}
export interface editableFieldDefinition {
    name: string;
    description?: string;
    code: string;
    type: editableDataType;
    isRequired: boolean;
    order: number;
    group?: string;
    fieldReference?: string,
    uniqueScope?: string,
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
    Reference,
    Code
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
export enum objectActionScope {
    New = 1,
    Edit = 2,
    All = 3
}