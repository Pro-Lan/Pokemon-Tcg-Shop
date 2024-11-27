<template>
  <div class="register-container">
    <h2>註冊帳戶</h2>
    <form @submit.prevent="register">
      <div class="form-group">
        <label for="username">用戶名</label>
        <input type="text" id="username" v-model="username" required>
      </div>
      <div class="form-group">
        <label for="email">電子郵件</label>
        <input type="email" id="email" v-model="email" required>
      </div>
      <div class="form-group">
        <label for="password">密碼</label>
        <input type="password" id="password" v-model="password" required>
      </div>
      <button type="submit" class="register-btn">註冊</button>
    </form>
    <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { registerUser } from '../services/api'

const username = ref('')
const email = ref('')
const password = ref('')
const errorMessage = ref('')

const register = async () => {
  try {
    const user = { username: username.value, email: email.value, password: password.value }
    await registerUser(user)
    alert('註冊成功！')
  } catch (error) {
    errorMessage.value = '註冊失敗：' + error.response.data
  }
}
</script>

<style scoped>
.register-container {
  max-width: 400px;
  width: 90%;
  margin: 20px auto;
  padding: 20px;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
  color: #333;
}

input {
  width: 100%;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 16px;
}

.register-btn {
  width: 100%;
  padding: 12px;
  background: #3498db;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  transition: background 0.3s ease;
}

.register-btn:hover {
  background: #2980b9;
}

.error-message {
  margin-top: 10px;
  color: #e74c3c;
  font-weight: bold;
}

@media (max-width: 768px) {
  .register-container {
    width: 95%;
    margin: 10px auto;
    padding: 15px;
  }

  input {
    padding: 10px;
    font-size: 14px;
  }

  .register-btn {
    padding: 10px;
    font-size: 14px;
  }
}

@media (max-width: 480px) {
  .register-container {
    width: 100%;
    margin: 0;
    border-radius: 0;
  }
}
</style> 