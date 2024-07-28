class ajax {

    constructor() {
    }

    static #run = (method: string, url: string, model?: any, responseType?: XMLHttpRequestResponseType) => {
        return new Promise((resolve, reject) => {
            var xhr = new XMLHttpRequest();
            if (responseType)
                xhr.responseType = responseType;
            xhr.onload = () => {
                if (xhr.status == 200) {
                    try {
                        
                        if (responseType == 'document') {
                            resolve(xhr.response);
                            return;
                        }

                        var obj = JSON.parse(xhr.responseText);
                        if (obj.successful)
                            resolve(obj);
                        else
                            reject(obj);
                    } catch {
                        reject();
                    }
                } else {
                    try {
                        var obj = JSON.parse(xhr.responseText);
                        reject(obj);
                    } catch {
                        reject(xhr.responseText);
                    }
                }
            }
            if (model) {
                xhr.open(method, url);
                xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8")
                xhr.send(JSON.stringify(model));
            } else {
                xhr.open(method, url, true);
                xhr.send();
            }
        })
    }

    static get(url: string, model?: any) {
        if (model) {
            var qs = Object.keys(model).map((key) => {
                return encodeURIComponent(key) + '=' + encodeURIComponent(model[key])
            }).join('&');
            url = url + (qs != '' ? '?' + qs : '')
        }
        return ajax.#run('get', url);
    }

    static getHtml(url, model?: any) {
        if (model) {
            var qs = Object.keys(model).map((key) => {
                return encodeURIComponent(key) + '=' + encodeURIComponent(model[key])
            }).join('&');
            url = url + (qs != '' ? '?' + qs : '')
        }
        return ajax.#run('get', url, {}, 'document');
    }

    static post(url, model) {
        return ajax.#run('post', url, model);
    }

    static put(url, model) {
        return ajax.#run('put', url, model);
    }

    static delete(url, model) {
        if (model) {
            var qs = Object.keys(model).map((key) => {
                return encodeURIComponent(key) + '=' + encodeURIComponent(model[key])
            }).join('&');
            url = url + (qs != '' ? '?' + qs : '')
        }
        return ajax.#run('delete', url);
    }

}

export { ajax };