<template>
    <div id="app" v-cloak>
        <!-- <authorization-page></authorization-page> -->
        <employee-page></employee-page>
    </div>
</template>

<script>
    import authorizationPage from './views/authorization-page.vue';
    import employeePage from './views/employee-page.vue';

    export default {
        name: 'app',
        components: {
            authorizationPage,
            employeePage
        }
    };
</script>

<style>
[v-cloak] {display: none}
</style>

