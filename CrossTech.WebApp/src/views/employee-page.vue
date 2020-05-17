<template>
    <div class="col-12" id="employee-app">

        <div class="col-8 d-flex justify-content-between">
            
            <div class="col-6 mt-4">
                <!-- @if (isAdmin) -->
                <!--
                    <div class="col-4" v-show="State==1">
                        <button class="text-white bg-primary form-control" v-on:click="State=2">
                            Редактировать
                        </button>
                    </div>
                    <div class="col-4 btn-group" v-show="State==2">
                        <button class="text-white bg-primary form-control" v-on:click="onClick_Save">
                            Сохранить
                        </button>
                        <button class="ml-3 text-white bg-primary form-control" v-on:click="onClick_Cancel">
                            Отменить
                        </button>
                    </div>
                -->
                <!-- @if (isAdmin) -->
            </div>
            
            <div class="col-2 mt-4">
                <form action="/authorization/logout">
                    <input class="text-white bg-primary form-control" type="submit" value="Выйти" />
                </form>
            </div>
            
        </div>
        
        <div class="btn-group mt-4 col-12">
            <div class="col-8">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col">#</th>
                            <th scope="col">Имя</th>
                            <th scope="col">Отчество</th>
                            <th scope="col">Фамилия</th>
                            <th scope="col">Пол</th>
                            <th scope="col">Должность</th>
                            <th scope="col">Дата рождения</th>
                            <th scope="col">Телефон</th>
                            <th scope="col"><span v-if="State==2">Удалить</span></th>
                        </tr>
                    </thead>
                    
                    <tbody v-if="State==1">
                        <tr v-for="employee in getEmployeesList" :key="employee.id">
                            <td>{{employee.id}}</td>
                            <td>{{employee.firstName}}</td>
                            <td>{{employee.fatherName}}</td>
                            <td>{{employee.lastName}}</td>
                            <td>{{employee.sex === 0 ? 'Мужчина' : 'Женщина'}}</td>
                            <td>{{getPositionNameById(employee.positionId)}}</td>
                            <td>{{getDate(employee)}}</td>
                            
                            <td>
                                <input type="text" 
                                       style="border: none;border-color: transparent;background-color:transparent;" 
                                       v-mask="'+7 (###) ###-##-##'"
                                       :value="employee.phone" disabled />
                            </td>
                        </tr>
                    </tbody>
                    
                    <!-- @if (isAdmin) -->
                    <!--
                        <tbody v-show="State==2">
                            <tr v-for="employee in getEmployeesList" :key="employee.id">
                                <td>{{employee.id}}</td>
                                <td><input type="text" class="form-control form-control-sm" v-model="employee.firstName" /></td>
                                <td><input type="text" class="form-control form-control-sm" v-model="employee.fatherName" /></td>

                                <td><input type="text" class="form-control form-control-sm" v-model="employee.lastName" /></td>
                                <td>
                                    <select class="form-control form-control-sm" v-model="employee.sex">
                                        <option value="0">Мужчина</option>
                                        <option value="1">Женщина</option>
                                    </select>
                                </td>

                                <td>
                                    <select class="form-control form-control-sm" v-model="employee.positionId">
                                        <option v-for="position in Positions" v-bind:value="position.id" :key="position.id">{{position.name}}</option>
                                    </select>
                                </td>
                                <td>
                                    <vuejs-datapicker :language="ru"
                                                      :input-class="'form-control form-control-sm bg-white'"
                                                      :format="'yyyy-MM-dd'"
                                                      :disabled-dates="{to: new Date(1940, 1, 1), from: new Date(2005, 12, 31)}"
                                                      v-model="employee.birthday">
                                    </vuejs-datapicker>
                                </td>
                                <td><input type="text" class="form-control form-control-sm" v-mask="'+7 (###) ###-##-##'" v-model="employee.phone" /></td>

                                <td>
                                    <button class="form-control form-control-sm bg-primary text-white" v-on:click="onClick_Delete(employee.id)">
                                        Удалить
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                                <td>
                                    <button class="form-control form-control-sm bg-primary text-white" v-on:click="onClick_Add">
                                        Добавить
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    -->
                    <!-- admin -->
                </table>
            </div>
            <div class="col-4">
                <div class="col-12">
                    <div class="mt-4 col-6">
                        <apexchart id="c-sex" :width="445" :options="getChartSexOptions" :series="getChartSexSeries"></apexchart>
                    </div>
                    <div class="mt-4 col-6">
                        <apexchart id="c-age" :options="getChartAgeOptions" :series="getChartAgeSeries"></apexchart>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Vue from 'vue'
import VueMask from 'v-mask'
Vue.use(VueMask);

import Datepicker from 'vuejs-datepicker';
import VueApexCharts from 'vue-apexcharts';
import moment from 'moment';

import crosstechApi from '../api/crosstech-api'


export default {
    name: "employee-page",
    components: {
        Datepicker,
        apexchart: VueApexCharts,
    },
    data: function() {
        return {
            //ru: vdp_translation_ru.js,
            Employees: [],
            DefaultEmployees: [],
            AccessToken: '',
            Positions: [],
            State: 1
        }
    },
    computed: {
        getEmployeesList: function () {

            if (this.Positions.length == 0) return [];

            return this.Employees;
        },

        getChartSexSeries: function () {
            var maleCount = this.DefaultEmployees.filter(x => x.sex == 0).length;
            var femaleCount = this.DefaultEmployees.length - maleCount;

            return [maleCount, femaleCount];
        },

        getChartSexOptions: function () {

            var labels = ['Мужчины', 'Женщины'];

            return {
                chart: {
                    width: 420,
                    type: 'pie',
                },
                labels: labels
            }
        },

        getChartAgeSeries: function () {

            var decadeDict = [];

            var decade = this.DefaultEmployees.map(function (x) {
                var y = moment(x.birthday).year();
                return y - y % 10;
            }).sort();

            decade.forEach(function (x) {
                if (decadeDict[x] == null) {
                    decadeDict[x] = 1;
                } else {
                    decadeDict[x]++;
                }
            });
            
            var arr = [];
            for (var key in decadeDict) {
                arr.push(decadeDict[key]);
            }

            return arr;
        },

        getChartAgeOptions: function () {

            let distinct = (value, index, self) => {
                return self.indexOf(value) === index;
            };

            var labels = this.DefaultEmployees.map(function (x) {
                var y = moment(x.birthday).year();
                return y - y % 10;
            }).filter(distinct).sort();

            return {
                chart: {
                    width: 420,
                    type: 'pie',
                },
                labels: labels
            }
        }
    },
    methods: {

        printMessage: function (text) {
            alert(text);
        },

        printRequestResult: function (r) {
            if (!r.isSuccess) {
                this.printMessage(r.message);
            }
        },

        getCopyArray: function (src) {
            return JSON.parse(JSON.stringify(src));
        },

        getDate: function (employee) {
            return moment(employee.birthday).format('YYYY-MM-DD');
        },

        getPhone: function (employee) {
            return employee.phone
                .replace(' ', '').replace(' ', '').replace(' ', '')
                .replace('-', '').replace('-', '').replace('-', '')
                .replace('(', '').replace(')', '');
        },

        getPositionNameById: function(id) {
            if (this.Positions.some(x => x.id === id))
                return this.Positions.find(x => x.id == id).name
            else
                return 'undefined';
        },

        normalizeEmployee: function (e) {
            e.phone = this.getPhone(e);
            e.birthday = this.getDate(e);
            e.sex = parseInt(e.sex);

            return e;
        },

        refreshEmployees: async function () {

            var employees = await this.getEmployeesAsync();

            this.DefaultEmployees = employees;
            this.Employees = this.getCopyArray(this.DefaultEmployees);
        },

        // events

        onClick_Save: async function () {

            var vue = this;

            var defaultEmployees = this.getCopyArray(this.DefaultEmployees);
            var employees = this.getCopyArray(this.Employees);

            defaultEmployees.forEach(x => x = vue.normalizeEmployee(x));
            employees.forEach(x => x = vue.normalizeEmployee(x));

            var deleteList = defaultEmployees.filter(x => !employees.some(y => y.id == x.id));
            var addList = employees.filter(x => !defaultEmployees.some(y => y.id == x.id));
            var updateList = employees.filter(function (x) {
                return defaultEmployees.some(function (y) {

                    return (x.id === y.id) &&
                        (
                            (x.firstName !== y.firstName)
                            || (x.fatherName !== y.fatherName)
                            || (x.lastName !== y.lastName)
                            || (x.sex !== y.sex)
                            || (x.positionId !== y.positionId)
                            || (x.birthday !== y.birthday)
                            || (x.phone !== y.phone)
                        );
                });
            });

            for (let i = 0; i < deleteList.length; i++) {
                let result = await vue.deleteEmployeeAsync(deleteList[i]);
                this.printRequestResult(result);
            }

            for (let i = 0; i < addList.length; i++) {
                let result = await vue.addEmployeeAsync(addList[i]);
                this.printRequestResult(result);
            }

            for (let i = 0; i < updateList.length; i++) {
                let result = await vue.updateEmployeeAsync(updateList[i]);
                this.printRequestResult(result);
            }

            await this.refreshEmployees();
            this.State = 1;
        },

        onClick_Cancel: function () {
            this.Employees = this.getCopyArray(this.DefaultEmployees);
            this.State = 1;
        },

        onClick_Delete: function (employeeId) {
            this.Employees = this.Employees.filter(x => x.id != employeeId);
        },

        onClick_Add: function () {
            this.Employees.push({
                id: -1,
                firstName: '',
                fatherName: '',
                lastName: '',
                sex: 0,
                positionId: 1,
                birthday: '',
                phone: ''
            });
        },

        onClick_Logout: function () {

        },

        // requests

        updateEmployeeAsync: async function (e) {

            var data = {
                Employee: e,
                accessToken: this.AccessToken
            };

            var response = await crosstechApi.employee.updateAsync(data);
            return response.data;
        },

        deleteEmployeeAsync: async function (e) {

            var data = {
                Id: e.id,
                accessToken: this.AccessToken
            };

            var response = await crosstechApi.employee.deleteAsync(data);
            return response.data;
        },

        addEmployeeAsync: async function (e) {

            var data = {
                Employee: e,
                accessToken: this.AccessToken
            };

            var response = await crosstechApi.employee.insertAsync(data);
            return response.data;
        },

        getEmployeesAsync: async function () {

            var data = {
                accessToken: this.AccessToken
            };

            var response = (await crosstechApi.employee.getAllAsync(data)).data;

            if (response.isSuccess)
                return response.employees;

            return [];
        },

        getPositionsAsync: async function () {

            var response = (await crosstechApi.position.getAllAsync()).data;

            if (response.isSuccess)
                return response.positions;

            return [];
        }
    },
    created: async function () {

        await this.refreshEmployees();
        this.Positions = await this.getPositionsAsync();
    }
}
</script>


<style>
</style>