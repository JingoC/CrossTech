var authApp = new Vue({
    el: '#auth-app',
    data: {
        Login: '',
        Password: ''
    },
    methods: {

        onClick_Login: async function () {

            var isAuth = await this.authAsync();

            if (isAuth) {
                window.location.href = redirectUrl;
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
                console.log(response);

                return response.isSuccess;
            } catch (e) {
                alert('Ошибка авторизации');
                return false;
            }
        }

    }
});