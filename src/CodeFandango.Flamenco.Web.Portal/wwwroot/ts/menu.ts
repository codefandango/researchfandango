import {ajax} from './ajax.js';

export class menu {

    bodyElement: any;
    menuElement: any;
    subMenu?: any;
    loadingTimer: any = 0;

    constructor(){
        this.bodyElement = document.querySelector('body');
        this.menuElement = document.querySelector('.menu');
        this.init();

        window.addEventListener('click', e => {   
            var sm = this.getSubMenu();
            if (this.menuElement.contains(e.target)) {
                return;
            }
            if (!sm.contains(e.target) && !sm.classList.contains('hide')) {
                sm.classList.add('hide');
                this.menuElement.querySelectorAll('a.menu-item').forEach(x=>x.classList.remove('active'));
            }
        });
    }

    init() {
        this.bind();
    }

    getSubMenu() {
        if (this.subMenu) {
            return this.subMenu;
        } else {
            this.subMenu = document.createElement('div');
            this.subMenu.classList.add('sub-menu');
            this.bodyElement.appendChild(this.subMenu);
            this.subMenu.classList.add('hide');
            return this.subMenu;
        }
    }

    loading(show: boolean) {
        clearTimeout(this.loadingTimer);
        var subMenu = this.getSubMenu();
        if (!show) {
            var loadingElement = subMenu.querySelector('.loading');
            if (loadingElement) {
                loadingElement.remove();
            }
            return;
        }
        this.loadingTimer = setTimeout(() => {
            subMenu.innerHTML = '<div class="loading"><i class="fas fa-spinner fa-spin fa-3x"></i></div>';
        }, 500);
    }

    loadSubMenuContent(subMenuId: any) {
        this.loading(true);
        this.getSubMenu().innerHTML = '';
        ajax.getHtml(`/menu/${subMenuId}`)
            .then((response: any) => {
                this.loading(false);
                this.getSubMenu().innerHTML = response.documentElement.querySelector('body').innerHTML;
            });
    }

    menuItemClick = (e: any) => {
        var subMenuId = e.currentTarget.getAttribute('data-submenu');
        if (subMenuId) {
            e.preventDefault(); 
            this.menuElement.querySelectorAll('a.menu-item').forEach(x=>x.classList.remove('active'));
            e.currentTarget.classList.add('active');
            var subMenu = this.getSubMenu();
            subMenu.classList.remove('hide');
            this.loadSubMenuContent(subMenuId);
        }
    }

    bind() {
        this.menuElement.querySelectorAll('a.menu-item').forEach(x=>{
            x.removeEventListener('click', this.menuItemClick);
            x.addEventListener('click', this.menuItemClick);
        });
    }

}

