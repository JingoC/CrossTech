import BaseApi from './base-api'

class IdentityApi extends BaseApi {

    constructor() {
        super('authorization');
    }

    async authAsync (data){
        return await this.postAsync(data);
    }
}

export default new IdentityApi();