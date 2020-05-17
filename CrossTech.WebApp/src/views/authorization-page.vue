<template>
    <div>
        <div class="mt-5 col-12 w-100 h-100"></div>
        <div class="mt-5 w-100 h-100">
            <div class="col-12  d-flex justify-content-center align-middle" id="auth-app">
                <div class="col-3 border border-light rounded-pill shadow-lg p-3 mb-5 bg-white rounded">
                    <div class="mt-2 w-100 d-flex justify-content-center">
                        <div class="col-8">
                            <input class="form-control text-center" type="text" v-model="Login" placeholder="Login" />
                        </div>
                    </div>
                    <div class="mt-2 w-100 d-flex justify-content-center">
                        <div class="col-8">
                            <input class="form-control text-center" type="text" v-model="Password" placeholder="Password" />
                        </div>

                    </div>
                    <div class="mt-2 mb-2 w-100 d-flex justify-content-center">
                        <div class="col-6">
                            <button class="form-control bg-primary text-center text-white" v-on:click="onClick_Login">Login</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import crosstechApi from '../api/crosstech-api'

    export default {
        name: "authorization-page",
        data() {
            return {
                Login: '',
                Password: ''
            }
        },
        methods: {

            // events 
            
            onClick_Login: async function() {

            },

            // request

            authAsync: async function () {

                var data = {
                    login: this.Login,
                    password: this.Password
                };

                try {
                    var response = await crosstechApi.identity.authAsync(data);
                    this.Message = response.data.message;

                    return response.data.isSuccess;
                } catch (e) {
                    return false;
                }
            }

        }
    }
</script>


<style>
</style>