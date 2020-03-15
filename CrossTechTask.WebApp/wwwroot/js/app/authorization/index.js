var authApp = new Vue({
    el: '#auth-app',
    data: {
        Login: '',
        Password: '',
        Message: ''
    },
    methods: {

        onClick_Login: async function () {

            var isAuth = await this.authAsync();

            if (isAuth) {
                window.location.href = redirectUrl;
            } else {
                alert(this.Message);
            }

        },

        // requests

        authAsync: async function () {

            var data = {
                login: this.Login,
                password: this.Password
            };

            try {
                var response = (await axios.post(authUrl, data)).data;
                this.Message = response.message;

                return response.isSuccess;
            } catch (e) {
                this.Message = 'Ошибка авторизации: ' + e;
                return false;
            }
        }

    }
});