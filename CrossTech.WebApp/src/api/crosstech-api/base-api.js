import config from '../../common/config'
import axios from 'axios'

class BaseApi{

    constructor(url){
        this.baseUrl = config.getCrossTechWebApiBaseUrl();
        this.url = url;
    }

    getUrl() {
        return `${this.baseUrl}/${this.url}`;
    }

    async getAsync(url='') {
        return (await axios.get(`${this.getUrl()}${url}`));
    }

    async postAsync(data, url='') {
        return await axios.post(`${this.getUrl()}${url}`, data);
    }

    async putAsync (data, url='') {
        return await axios.put(`${this.getUrl()}${url}`, data);
    }

    async deleteAsync (data, url='') {
        return await axios.delete(`${this.getUrl()}${url}`, {
            headers: { 'Content-Type': 'application/json' },
            data: JSON.stringify(data)
        });
    }
}

export default BaseApi;