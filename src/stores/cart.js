import { defineStore } from 'pinia'

export const useCartStore = defineStore('cart', {
  state: () => ({
    items: []
  }),
  
  getters: {
    totalItems: (state) => state.items.reduce((sum, item) => sum + item.quantity, 0),
    totalAmount: (state) => state.items.reduce((sum, item) => sum + item.price * item.quantity, 0)
  },
  
  actions: {
    addToCart(card) {
      const existingItem = this.items.find(item => item.id === card.id)
      if (existingItem) {
        existingItem.quantity++
      } else {
        this.items.push({
          ...card,
          quantity: 1
        })
      }
    },
    
    removeFromCart(cardId) {
      const index = this.items.findIndex(item => item.id === cardId)
      if (index > -1) {
        this.items.splice(index, 1)
      }
    },
    
    updateQuantity(cardId, quantity) {
      const item = this.items.find(item => item.id === cardId)
      if (item) {
        item.quantity = quantity
      }
    },
    
    clearCart() {
      this.items = []
    }
  }
}) 