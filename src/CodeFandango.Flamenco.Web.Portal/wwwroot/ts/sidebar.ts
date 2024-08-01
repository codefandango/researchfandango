import { Offcanvas } from 'bootstrap/dist/js/bootstrap';
import { sidebarButtonDefinition, sidebarButtonCollection, sidebarConfiguration } from './objectEditorTypes';

export class sidebar {

    element: HTMLObjectElement | HTMLElement | HTMLAnchorElement | HTMLAreaElement | HTMLAudioElement | HTMLBaseElement | HTMLQuoteElement | HTMLBodyElement | HTMLBRElement | HTMLButtonElement | HTMLCanvasElement | HTMLTableCaptionElement | HTMLTableColElement | HTMLDataElement | HTMLDataListElement | HTMLModElement | HTMLDetailsElement | HTMLDialogElement | HTMLDivElement | HTMLDListElement | HTMLEmbedElement | HTMLFieldSetElement | HTMLFormElement | HTMLHeadingElement | HTMLHeadElement | HTMLHRElement | HTMLHtmlElement | HTMLIFrameElement | HTMLImageElement | HTMLInputElement | HTMLLabelElement | HTMLLegendElement | HTMLLIElement | HTMLLinkElement | HTMLMapElement | HTMLMenuElement | HTMLMetaElement | HTMLMeterElement | HTMLOListElement | HTMLOptGroupElement | HTMLOptionElement | HTMLOutputElement | HTMLParagraphElement | HTMLPictureElement | HTMLPreElement | HTMLProgressElement | HTMLScriptElement | HTMLSelectElement | HTMLSlotElement | HTMLSourceElement | HTMLSpanElement | HTMLStyleElement | HTMLTableElement | HTMLTableSectionElement | HTMLTableCellElement | HTMLTemplateElement | HTMLTextAreaElement | HTMLTimeElement | HTMLTitleElement | HTMLTableRowElement | HTMLTrackElement | HTMLUListElement | HTMLVideoElement;
    Offcanvas: any;
    bodyElement: any;
    id: string;
    size = 'xl';

    sizes = {
        'xl': '50',
        'lg': '40',
        'md': '30',
        'sm': '20',
        'xs': '10'
    }

    rootConfig: any = {
        backdrop: 'static'
    }

    buttons: sidebarButtonCollection = {};
    onClose: Function;

    constructor(config?: sidebarConfiguration) {
        this.onClose = () => { };
        if (config?.onClose) this.onClose = config.onClose;
        this.rootConfig = config;
        this.id = crypto.randomUUID();
        var elementId: any = this.#addToPage();
        this.element = document.querySelector(elementId);
        this.Offcanvas = new Offcanvas(this.element);
        this.bodyElement = this.element.querySelector('.offcanvas-body');
        this.bodyElement.innerHTML = '';
        this.element.querySelector('.btn-close').addEventListener('click', this.close);

        this.buttons['close'] = {
            text: 'Close',
            icon: 'fas fa-times',
            click: this.close,
            order: 2
        };
    }

    beforeClose(cb) {
        cb(true);
    }

    close = (force) => {
        if (force === true) {
            this.onClose();
            this.Offcanvas.hide();
            return;
        }
        this.beforeClose(x=>{
            if (x) {
                this.onClose();
                this.Offcanvas.hide();
            }
        })
    }

    #addToPage() {
        var html = `<div class="offcanvas offcanvas-end w-${this.sizes[this.size]}" id="sidebar-${this.id}" data-bs-backdrop="${this.rootConfig?.backdrop || 'static'}" tabindex="-1" aria-labelledby="offcanvasLabel">
            <div class="offcanvas-header">
                <h5 class="offcanvas-title" id=""></h5>
                <button type="button" class="btn-close" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
            </div>
        </div>`;
        var idSelector = `#sidebar-${this.id}`;
        var body = document.querySelector('body');
        const parser = new DOMParser();
        const htmlDoc = parser.parseFromString(html, 'text/html');
        body.append(htmlDoc.documentElement.querySelector(idSelector));
        return idSelector;        
    }

    render(): string {

        var html = ``;

        // Render the buttons
        html += `
            <div class="toolbar">
                <div class="btn-group" role="group">`;
        Object.keys(this.buttons).sort((a, b) => this.buttons[a].order - this.buttons[b].order).forEach(x => {
            html += `<button type="button" class="btn toolbar-btn" onclick="${this.buttons[x].click}" data-button-id="${x}">
                <div class="icon"><i class="${this.buttons[x].icon}"></i></div>
                <label>${this.buttons[x].text}</label>
            </button>`;
        });
        html += `</div></div>`;

        return html;

    }

    addButton(key: string, button: sidebarButtonDefinition) {
        this.buttons[key] = button;
        this.render();
    }

    bind() {
        this.bodyElement.querySelectorAll('.toolbar-btn').forEach((x: any) => {
            x.removeEventListener('click', this.toolbarButtonClick);
            x.addEventListener('click', this.toolbarButtonClick);
        });
    }

    toolbarButtonClick = (e: any) => {
        var buttonId = e.currentTarget.getAttribute('data-button-id');
        this.buttons[buttonId].click(e);
    }
    
    show = (e?: any) => {
        this.Offcanvas.show();
    }

}