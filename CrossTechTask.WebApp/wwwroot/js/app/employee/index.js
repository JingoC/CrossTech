dictinct = (value, index, self) => {
    return self.indexOf(value) === index;
};

Vue.use(VueMask.VueMaskPlugin);

var employeeApp = new Vue({
    el: '#employee-app',
    components: {
        'vuejs-datapicker': vuejsDatepicker,
        apexchart: VueApexCharts,
    },
    data: {
        ru: vdp_translation_ru.js,
        Employees: [],
        DefaultEmployees: [],
        AccessToken: '',
        Positions: [],
        State: 1
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

            var labels = this.DefaultEmployees.map(function (x) {
                var y = moment(x.birthday).year();
                return y - y % 10;
            }).filter(dictinct).sort();

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

            for (var i = 0; i < deleteList.length; i++) {
                var result = await vue.deleteEmployeeAsync(deleteList[i]);
                this.printRequestResult(result);
            }

            for (var i = 0; i < addList.length; i++) {
                var result = await vue.addEmployeeAsync(addList[i]);
                this.printRequestResult(result);
            }

            for (var i = 0; i < updateList.length; i++) {
                var result = await vue.updateEmployeeAsync(updateList[i]);
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

            try {

                var response = await axios.post(updateEmployeeUrl, data);
                var responseData = response.data;

                return responseData;
            } catch (e) {
                return {
                    isSuccess: false,
                    message: 'Не удалось обновить сотрудника: ' + e
                };
            }
        },

        deleteEmployeeAsync: async function (e) {

            var data = {
                Id: e.id,
                accessToken: this.AccessToken
            };

            try {

                var response = await axios.post(deleteEmployeeUrl, data);
                var responseData = response.data;

                return responseData.isSuccess;
            } catch (e) {
                return {
                    isSuccess: false,
                    message: 'Не удалось удалить сотрудника: ' + e
                };
            }

        },

        addEmployeeAsync: async function (e) {

            var data = {
                Employee: e,
                accessToken: this.AccessToken
            };

            try {

                var response = await axios.post(addEmployeeUrl, data);
                var responseData = response.data;

                return responseData.isSuccess;
            } catch (e) {
                return {
                    isSuccess: false,
                    message: 'Не удалось добавить сотрудника: ' + e
                };
            }

        },

        getEmployeesAsync: async function () {

            var data = {
                accessToken: this.AccessToken
            };

            try {

                var response = (await axios.post(getEmployeesUrl, data)).data;
                
                if (response.isSuccess) {
                    return response.employees;
                } else {
                    alert(response.message);
                }

            } catch (e) {
                alert('Не удалось получить список сотрудников');
            }
        },

        getPositionsAsync: async function () {

            try {

                var response = (await axios.get(getPositionsUrl)).data;

                if (response.isSuccess) {
                    return response.positions;
                } else {
                    alert(response.message);
                }
            } catch (e) {
                alert('Не удалось получить список должностей');
            }

            return [];
        }
    },
    created: async function () {

        await this.refreshEmployees();
        this.Positions = await this.getPositionsAsync();
    }
});
