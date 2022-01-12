import { createApp } from 'vue'
import { createPinia } from 'pinia'
import axios from 'axios'
import VueAxios from 'vue-axios'
import { createHead } from '@vueuse/head'

import './assets/index.css'

import App from './App.vue'
import router from './router'

const app = createApp(App)
const head = createHead()

app.use(createPinia())
app.use(router)
app.use(head)

axios.defaults.headers.common['Content-Type'] = 'application/json'
axios.defaults.baseURL = import.meta.env.VITE_SERVER_URL

app.use(VueAxios, axios)
app.provide('axios', app.config.globalProperties.axios)

app.mount('#app')
