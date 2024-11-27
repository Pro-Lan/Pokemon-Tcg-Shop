<template>
  <div class="cart-container">
    <h2>購物車</h2>
    <div v-if="cartStore.items.length === 0" class="empty-cart">
      購物車是空的
    </div>
    <div v-else>
      <div class="cart-items">
        <div v-for="item in cartStore.items" :key="item.id" class="cart-item">
          <img :src="item.imageUrl" :alt="item.name" class="item-image">
          <div class="item-details">
            <h3>{{ item.name }}</h3>
            <p class="price">NT$ {{ item.price }}</p>
          </div>
          <div class="item-quantity">
            <button @click="decreaseQuantity(item)" class="quantity-btn">-</button>
            <span>{{ item.quantity }}</span>
            <button @click="increaseQuantity(item)" class="quantity-btn">+</button>
          </div>
          <div class="item-total">
            NT$ {{ (item.price * item.quantity) }}
          </div>
          <button @click="removeFromCart(item.id)" class="remove-btn">×</button>
        </div>
      </div>
      <div class="cart-summary">
        <div class="total">
          總計: NT$ {{ cartStore.totalAmount }}
        </div>
        <button @click="checkout" class="checkout-btn">結帳</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useCartStore } from '../stores/cart'
import { createOrder } from '../services/api'

const cartStore = useCartStore()
const checkoutSuccess = ref(false)

const increaseQuantity = (item) => {
  cartStore.updateQuantity(item.id, item.quantity + 1)
}

const decreaseQuantity = (item) => {
  if (item.quantity > 1) {
    cartStore.updateQuantity(item.id, item.quantity - 1)
  }
}

const removeFromCart = (itemId) => {
  cartStore.removeFromCart(itemId)
}

const checkout = async () => {
  try {
    const order = {
      items: cartStore.items,
      totalAmount: cartStore.totalAmount
    }
    await createOrder(order)
    cartStore.clearCart()
    checkoutSuccess.value = true
  } catch (error) {
    console.error('結帳失敗:', error)
  }
}
</script>

<style scoped>
.cart-container {
  max-width: 1000px;
  width: 90%;
  margin: 0 auto;
  padding: 20px;
}

.cart-items {
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.cart-item {
  display: flex;
  align-items: center;
  padding: 20px;
  border-bottom: 1px solid #eee;
  flex-wrap: wrap;
  gap: 10px;
}

.item-image {
  width: 80px;
  height: 80px;
  object-fit: contain;
}

.item-details {
  flex: 1;
  min-width: 200px;
}

.item-details h3 {
  margin: 0 0 5px 0;
  color: #2c3e50;
}

.price {
  color: #e74c3c;
  font-weight: bold;
}

.item-quantity {
  display: flex;
  align-items: center;
  gap: 10px;
}

.quantity-btn {
  width: 30px;
  height: 30px;
  border: 1px solid #ddd;
  background: white;
  border-radius: 4px;
  cursor: pointer;
}

.quantity-btn:hover {
  background: #f8f9fa;
}

.item-total {
  font-weight: bold;
  min-width: 100px;
  text-align: right;
}

.remove-btn {
  width: 30px;
  height: 30px;
  border: none;
  background: none;
  color: #e74c3c;
  font-size: 20px;
  cursor: pointer;
  margin-left: 20px;
}

.remove-btn:hover {
  color: #c0392b;
}

.cart-summary {
  margin-top: 20px;
  padding: 20px;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 10px;
}

.total {
  font-size: 1.2em;
  font-weight: bold;
}

.checkout-btn {
  padding: 10px 30px;
  background: #2ecc71;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 1.1em;
  transition: background 0.3s ease;
}

.checkout-btn:hover {
  background: #27ae60;
}

.checkout-success {
  margin-top: 20px;
  padding: 20px;
  background: #dff0d8;
  color: #3c763d;
  border-radius: 8px;
  text-align: center;
  font-weight: bold;
}

@media (max-width: 768px) {
  .cart-container {
    width: 95%;
    padding: 10px;
  }

  .cart-item {
    padding: 15px;
  }

  .item-details h3 {
    font-size: 1em;
  }

  .cart-summary {
    flex-direction: column;
    text-align: center;
  }

  .checkout-btn {
    width: 100%;
  }
}

@media (max-width: 480px) {
  .cart-item {
    flex-direction: column;
    align-items: flex-start;
  }

  .item-image {
    width: 100%;
    height: auto;
    margin-bottom: 10px;
  }

  .item-quantity {
    width: 100%;
    justify-content: center;
    margin: 10px 0;
  }

  .item-total {
    width: 100%;
    text-align: center;
  }

  .remove-btn {
    align-self: flex-end;
  }
}
</style> 