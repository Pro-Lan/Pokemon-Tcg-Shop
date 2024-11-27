<template>
  <div class="orders-container">
    <h2>訂單歷史</h2>
    <div v-if="orders.length === 0" class="empty-orders">
      尚無訂單記錄
    </div>
    <div v-else>
      <div v-for="order in orders" :key="order.orderId" class="order-item">
        <h3>訂單編號: {{ order.orderId }}</h3>
        <p>訂單日期: {{ formatDate(order.orderDate) }}</p>
        <p>總金額: NT$ {{ order.totalAmount.toFixed(2) }}</p>
        <div class="order-items">
          <div v-for="item in order.items" :key="item.id" class="order-item-detail">
            <img :src="item.imageUrl" :alt="item.name" class="item-image">
            <div class="item-info">
              <h4>{{ item.name }}</h4>
              <p>數量: {{ item.quantity }}</p>
              <p>價格: NT$ {{ item.price }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { getOrders } from '../services/api'

const orders = ref([])

const formatDate = (dateString) => {
  const date = new Date(dateString)
  return date.toLocaleDateString('zh-TW')
}

onMounted(async () => {
  try {
    const response = await getOrders()
    orders.value = response.data
  } catch (error) {
    console.error('獲取訂單失敗:', error)
  }
})
</script>

<style scoped>
.orders-container {
  max-width: 1000px;
  margin: 0 auto;
  padding: 20px;
}

.empty-orders {
  text-align: center;
  padding: 40px;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.order-item {
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
  padding: 20px;
}

.order-items {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  margin-top: 20px;
}

.order-item-detail {
  display: flex;
  align-items: center;
  gap: 15px;
  background: #f9f9f9;
  padding: 10px;
  border-radius: 8px;
}

.item-image {
  width: 60px;
  height: 60px;
  object-fit: contain;
}

.item-info {
  flex: 1;
}

.item-info h4 {
  margin: 0;
  font-size: 1em;
  color: #2c3e50;
}

.item-info p {
  margin: 5px 0;
  font-size: 0.9em;
  color: #666;
}
</style> 