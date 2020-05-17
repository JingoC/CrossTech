export default{

    config: null,
    getConfig: function() {

        if (process.env.NODE_ENV === 'development') {
            this.config = {
                crossTechWebApiBaseUrl: 'http://localhost:54000'
            }
        }else if (process.env.NODE_ENV === 'production') {
            this.config = {
                crossTechWebApiBaseUrl: ''
            }
        }else {
            throw `env not found: ${process.env.NODE_ENV}`;
        }

        return this.config;
    },

    getCrossTechWebApiBaseUrl: function() {
        return this.config ? this.config.crossTechWebApiBaseUrl : this.getConfig().crossTechWebApiBaseUrl;
    }
}