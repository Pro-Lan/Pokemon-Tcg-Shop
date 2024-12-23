import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', {
  state: () => ({
    currentUser: null
  }),
  
  getters: {
    isLoggedIn: (state) => !!state.currentUser,
    username: (state) => state.currentUser?.username
  },
  
  actions: {
    setUser(user) {
      this.currentUser = user
    },
    
    logout() {
      this.currentUser = null
    }
  }
}) 