var ui = (() => {
  var __defProp = Object.defineProperty;
  var __getOwnPropDesc = Object.getOwnPropertyDescriptor;
  var __getOwnPropNames = Object.getOwnPropertyNames;
  var __hasOwnProp = Object.prototype.hasOwnProperty;
  var __typeError = (msg) => {
    throw TypeError(msg);
  };
  var __defNormalProp = (obj, key, value) => key in obj ? __defProp(obj, key, { enumerable: true, configurable: true, writable: true, value }) : obj[key] = value;
  var __export = (target, all) => {
    for (var name in all)
      __defProp(target, name, { get: all[name], enumerable: true });
  };
  var __copyProps = (to, from, except, desc) => {
    if (from && typeof from === "object" || typeof from === "function") {
      for (let key of __getOwnPropNames(from))
        if (!__hasOwnProp.call(to, key) && key !== except)
          __defProp(to, key, { get: () => from[key], enumerable: !(desc = __getOwnPropDesc(from, key)) || desc.enumerable });
    }
    return to;
  };
  var __toCommonJS = (mod) => __copyProps(__defProp({}, "__esModule", { value: true }), mod);
  var __publicField = (obj, key, value) => __defNormalProp(obj, typeof key !== "symbol" ? key + "" : key, value);
  var __accessCheck = (obj, member, msg) => member.has(obj) || __typeError("Cannot " + msg);
  var __privateGet = (obj, member, getter) => (__accessCheck(obj, member, "read from private field"), getter ? getter.call(obj) : member.get(obj));
  var __privateAdd = (obj, member, value) => member.has(obj) ? __typeError("Cannot add the same private member more than once") : member instanceof WeakSet ? member.add(obj) : member.set(obj, value);

  // ts/menu.ts
  var menu_exports = {};
  __export(menu_exports, {
    menu: () => menu
  });

  // ts/ajax.ts
  var _run;
  var _ajax = class _ajax {
    constructor() {
    }
    static get(url, model) {
      var _a;
      if (model) {
        var qs = Object.keys(model).map((key) => {
          return encodeURIComponent(key) + "=" + encodeURIComponent(model[key]);
        }).join("&");
        url = url + (qs != "" ? "?" + qs : "");
      }
      return __privateGet(_a = _ajax, _run).call(_a, "get", url);
    }
    static getHtml(url, model) {
      var _a;
      if (model) {
        var qs = Object.keys(model).map((key) => {
          return encodeURIComponent(key) + "=" + encodeURIComponent(model[key]);
        }).join("&");
        url = url + (qs != "" ? "?" + qs : "");
      }
      return __privateGet(_a = _ajax, _run).call(_a, "get", url, {}, "document");
    }
    static post(url, model) {
      var _a;
      return __privateGet(_a = _ajax, _run).call(_a, "post", url, model);
    }
    static put(url, model) {
      var _a;
      return __privateGet(_a = _ajax, _run).call(_a, "put", url, model);
    }
    static delete(url, model) {
      var _a;
      if (model) {
        var qs = Object.keys(model).map((key) => {
          return encodeURIComponent(key) + "=" + encodeURIComponent(model[key]);
        }).join("&");
        url = url + (qs != "" ? "?" + qs : "");
      }
      return __privateGet(_a = _ajax, _run).call(_a, "delete", url);
    }
  };
  _run = new WeakMap();
  __privateAdd(_ajax, _run, (method, url, model, responseType) => {
    return new Promise((resolve, reject) => {
      var xhr = new XMLHttpRequest();
      if (responseType)
        xhr.responseType = responseType;
      xhr.onload = () => {
        if (xhr.status == 200) {
          try {
            if (responseType == "document") {
              resolve(xhr.response);
              return;
            }
            var obj = JSON.parse(xhr.responseText);
            if (obj.successful)
              resolve(obj);
            else
              reject(obj);
          } catch (e) {
            reject();
          }
        } else {
          try {
            var obj = JSON.parse(xhr.responseText);
            reject(obj);
          } catch (e) {
            reject(xhr.responseText);
          }
        }
      };
      if (model) {
        xhr.open(method, url);
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        xhr.send(JSON.stringify(model));
      } else {
        xhr.open(method, url, true);
        xhr.send();
      }
    });
  });
  var ajax = _ajax;

  // ts/menu.ts
  var menu = class {
    constructor() {
      __publicField(this, "bodyElement");
      __publicField(this, "menuElement");
      __publicField(this, "subMenu");
      __publicField(this, "loadingTimer", 0);
      __publicField(this, "menuItemClick", (e) => {
        var subMenuId = e.currentTarget.getAttribute("data-submenu");
        if (subMenuId) {
          e.preventDefault();
          this.menuElement.querySelectorAll("a.menu-item").forEach((x) => x.classList.remove("active"));
          e.currentTarget.classList.add("active");
          var subMenu = this.getSubMenu();
          subMenu.classList.remove("hide");
          this.loadSubMenuContent(subMenuId);
        }
      });
      this.bodyElement = document.querySelector("body");
      this.menuElement = document.querySelector(".menu");
      this.init();
      window.addEventListener("click", (e) => {
        var sm = this.getSubMenu();
        if (this.menuElement.contains(e.target)) {
          return;
        }
        if (!sm.contains(e.target) && !sm.classList.contains("hide")) {
          sm.classList.add("hide");
          this.menuElement.querySelectorAll("a.menu-item").forEach((x) => x.classList.remove("active"));
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
        this.subMenu = document.createElement("div");
        this.subMenu.classList.add("sub-menu");
        this.bodyElement.appendChild(this.subMenu);
        this.subMenu.classList.add("hide");
        return this.subMenu;
      }
    }
    loading(show) {
      clearTimeout(this.loadingTimer);
      var subMenu = this.getSubMenu();
      if (!show) {
        var loadingElement = subMenu.querySelector(".loading");
        if (loadingElement) {
          loadingElement.remove();
        }
        return;
      }
      this.loadingTimer = setTimeout(() => {
        subMenu.innerHTML = '<div class="loading"><i class="fas fa-spinner fa-spin fa-3x"></i></div>';
      }, 500);
    }
    loadSubMenuContent(subMenuId) {
      this.loading(true);
      this.getSubMenu().innerHTML = "";
      ajax.getHtml("/menu/".concat(subMenuId)).then((response) => {
        this.loading(false);
        this.getSubMenu().innerHTML = response.documentElement.querySelector("body").innerHTML;
      });
    }
    bind() {
      this.menuElement.querySelectorAll("a.menu-item").forEach((x) => {
        x.removeEventListener("click", this.menuItemClick);
        x.addEventListener("click", this.menuItemClick);
      });
    }
  };
  return __toCommonJS(menu_exports);
})();
