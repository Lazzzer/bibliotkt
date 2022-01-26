import { defineStore } from 'pinia'

export const useEmployeStore = defineStore({
  id: 'employe',
  state: () => ({
    logged: false
  }),
  getters: {
    isLogged: (state) => state.logged
  },
  actions: {
    connect() {
      this.logged = true;
    },
    disconnect() {
      this.logged = false;
    }
  }
})
