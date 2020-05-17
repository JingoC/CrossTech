import BaseApi from './base-api'

class EmployeeApi extends BaseApi {

    constructor() {
        super('employee')
    }

    async getAllAsync() {
        return await this.getAsync();
    }

    async updateAsync(data) {
        return await this.putAsync(data);
    }

    async deleteAsync(data) {
        return await this.deleteAsync(data);
    }

    async insertAsync(data) {
        return await this.post(data);
    }
}

export default new EmployeeApi();