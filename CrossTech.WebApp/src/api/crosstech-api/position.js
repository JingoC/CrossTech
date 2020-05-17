import BaseApi from './base-api'

class PositionApi extends BaseApi {

    constructor() {
        super('position');
    }

    async getAllAsync() {
        return await this.getAsync('/all');
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

export default new PositionApi();