<template>
  <nav class="navbar">
    <div class="nav-left">
      <router-link to="/" class="nav-brand">掰卡</router-link>
      <router-link to="/shop" class="nav-item">商店</router-link>
      <router-link to="/cart" class="nav-item">
        購物車
        <span v-if="cartStore.totalItems" class="cart-badge">
          {{ cartStore.totalItems }}
        </span>
      </router-link>
      <router-link to="/orders" class="nav-item">訂單</router-link>
    </div>
    <div class="nav-right">
      <template v-if="userStore.isLoggedIn">
        <span class="username">{{ userStore.username }}</span>
        <span class="nav-separator">|</span>
        <button @click="logout" class="nav-item logout-btn">登出</button>
      </template>
      <template v-else>
        <router-link to="/register" class="nav-item">註冊</router-link>
        <span class="nav-separator">|</span>
        <router-link to="/login" class="nav-item">登入</router-link>
      </template>
    </div>
  </nav>
</template>

<script setup>
import { useRouter } from 'vue-router'
import { useCartStore } from '../stores/cart'
import { useUserStore } from '../stores/user'

const router = useRouter()
const cartStore = useCartStore()
const userStore = useUserStore()

const logout = () => {
  userStore.logout()
  router.push('/login')
}
</script>

<style scoped>
.navbar {
  background-color: #ffffff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 1rem 2rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 1000;
}

.nav-left {
  display: flex;
  align-items: center;
  gap: 2rem;
}

.nav-brand {
  font-size: 1.5rem;
  font-weight: bold;
  color: #2c3e50;
  text-decoration: none;
}

.nav-item {
  color: #666;
  text-decoration: none;
  font-size: 1rem;
  transition: color 0.3s ease;
  position: relative;
}

.nav-item:hover {
  color: #3498db;
}

.nav-right {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.nav-separator {
  color: #ddd;
  font-size: 1rem;
}

.nav-right .nav-item {
  padding: 0.5rem 1rem;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.nav-right .nav-item:hover {
  background-color: #f8f9fa;
}

.cart-badge {
  background: #e74c3c;
  color: white;
  border-radius: 50%;
  padding: 2px 6px;
  font-size: 12px;
  position: absolute;
  top: -8px;
  right: -8px;
}

.username {
  color: #2c3e50;
  font-weight: 500;
  margin-right: 10px;
}

.logout-btn {
  background: none;
  border: 1px solid #ddd;
  padding: 0.5rem 1rem;
  border-radius: 4px;
  cursor: pointer;
  color: #666;
  font-size: 1rem;
  transition: all 0.3s ease;
}

.logout-btn:hover {
  background-color: #f8f9fa;
  color: #3498db;
}
</style> 