<template>
    <div class="home-container">
      <!-- 頁面標題區域 -->
      <header class="header">
        <h1>寶可夢卡牌商店</h1>
        <div class="filter-section">
          <div class="filter-container">
            <div class="search-bar">
              <i class="search-icon">🔍</i>
              <input 
                type="text" 
                v-model="searchQuery" 
                placeholder="搜索卡牌..."
              >
            </div>
            <div class="divider"></div>
            <div class="price-filter">
              <div class="price-inputs">
                <div class="input-group">
                  <label>最低價格</label>
                  <input 
                    type="number" 
                    v-model="minPrice" 
                    placeholder="NT$"
                    min="0"
                  >
                </div>
                <span class="price-separator">~</span>
                <div class="input-group">
                  <label>最高價格</label>
                  <input 
                    type="number" 
                    v-model="maxPrice" 
                    placeholder="NT$"
                    min="0"
                  >
                </div>
              </div>
              <button class="filter-button" @click="applyFilters">
                <span>篩選</span>
              </button>
            </div>
          </div>
        </div>
      </header>
  
      <!-- 卡牌列表區域 -->
      <main class="main-content">
        <!-- 載入狀態 -->
        <div v-if="loading" class="loading">
          <div class="spinner"></div>
          <p>正在載入卡牌...</p>
        </div>
  
        <!-- 錯誤提示 -->
        <div v-else-if="error" class="error-message">
          {{ error }}
        </div>
  
        <!-- 卡牌網格 -->
        <div v-else class="card-grid">
          <div 
            v-for="card in cards" 
            :key="card.id" 
            class="card-item"
            :class="{ 'sold-out': !card.isAvailable }"
          >
            <div class="card-image">
              <img :src="card.imageUrl" :alt="card.name" loading="lazy">
              <div v-if="!card.isAvailable" class="sold-out-overlay">已售完</div>
            </div>
            <div class="card-info">
              <h3>{{ card.name }}</h3>
              <p class="price">NT$ {{ card.price }}</p>
              <p class="date">上架日期: {{ formatDate(card.listedDate) }}</p>
              <button 
                class="buy-button" 
                :disabled="!card.isAvailable || !userStore.isLoggedIn"
                @click="addToCart(card)"
              >
                {{ getBuyButtonText(card) }}
              </button>
            </div>
          </div>
        </div>
      </main>
      <div v-if="showSuccessMessage" class="success-message">
        成功加入購物車！
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted, watch } from 'vue'
  import { useRouter } from 'vue-router'
  import { getCards } from '../services/api'
  import { useCartStore } from '../stores/cart'
  import { useUserStore } from '../stores/user'
  
  const router = useRouter()
  const cartStore = useCartStore()
  const userStore = useUserStore()
  const showSuccessMessage = ref(false)
  
  const cards = ref([])
  const loading = ref(true)
  const error = ref(null)
  const searchQuery = ref('')
  const minPrice = ref('')
  const maxPrice = ref('')
  
  const formatDate = (dateString) => {
    const date = new Date(dateString)
    return date.toLocaleDateString('zh-TW')
  }
  
  const getBuyButtonText = (card) => {
    if (!card.isAvailable) return '已售完'
    if (!userStore.isLoggedIn) return '請先登入'
    return '加入購物車'
  }
  
  const addToCart = (card) => {
    if (!userStore.isLoggedIn) {
      router.push('/login')
      return
    }
    cartStore.addToCart(card)
    showSuccessMessage.value = true
    setTimeout(() => {
      showSuccessMessage.value = false
    }, 2000) // 2秒後自動隱藏
  }
  
  // 添加防抖函數
  const debounce = (fn, delay) => {
    let timeout
    return (...args) => {
      clearTimeout(timeout)
      timeout = setTimeout(() => fn.apply(this, args), delay)
    }
  }
  
  const applyFilters = async () => {
    try {
      loading.value = true
      error.value = null
      const params = {
        search: searchQuery.value,
        minPrice: minPrice.value || null,
        maxPrice: maxPrice.value || null
      }
      const response = await getCards(params)
      cards.value = response.data
    } catch (err) {
      console.error('過濾卡牌失敗:', err)
      error.value = '過濾卡牌失敗: ' + err.message
    } finally {
      loading.value = false
    }
  }
  
  // 修改搜索函數
  const searchCards = async () => {
    await applyFilters()
  }
  
  // 使用防抖處理搜索
  const debouncedSearch = debounce(searchCards, 300)
  
  // 監聽搜索輸入
  watch(searchQuery, () => {
    debouncedSearch()
  })
  
  onMounted(async () => {
    try {
      console.log('開始獲取卡牌數據')
      const response = await getCards()
      console.log('獲取到的數據:', response.data)
      cards.value = response.data
      loading.value = false
    } catch (error) {
      console.error('獲取卡牌失敗:', error)
      error.value = '獲取卡牌失敗: ' + error.message
      loading.value = false
    }
  })
  </script>
  
  <style scoped>
  .home-container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
  }
  
  .header {
    text-align: center;
    margin-bottom: 20px;
  }
  
  .header h1 {
    font-size: 2.5em;
    color: #2c3e50;
    margin-bottom: 20px;
  }
  
  .search-bar {
    max-width: 500px;
    margin: 0 auto;
    margin-bottom: 20px;
  }
  
  .search-bar input {
    width: 100%;
    padding: 12px 20px;
    border: 2px solid #ddd;
    border-radius: 25px;
    font-size: 16px;
    transition: all 0.3s ease;
  }
  
  .search-bar input:focus {
    outline: none;
    border-color: #3498db;
    box-shadow: 0 0 5px rgba(52, 152, 219, 0.3);
  }
  
  .main-content {
    min-height: 400px;
  }
  
  .card-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
    gap: 25px;
    padding: 0;
  }
  
  .card-item {
    background: white;
    border-radius: 15px;
    overflow: hidden;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    cursor: pointer;
  }
  
  .card-item:hover {
    transform: translateY(-5px);
    box-shadow: 0 5px 20px rgba(0, 0, 0, 0.15);
  }
  
  .card-image {
    position: relative;
    padding-top: 100%;
  }
  
  .card-image img {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: contain;
  }
  
  .sold-out-overlay {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.5);
    color: white;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 1.5rem;
    font-weight: bold;
  }
  
  .card-info {
    padding: 15px;
    text-align: center;
  }
  
  .card-info h3 {
    margin: 0 0 10px;
    font-size: 1.1em;
    color: #2c3e50;
  }
  
  .price {
    font-size: 1.2em;
    color: #e74c3c;
    font-weight: bold;
    margin: 5px 0;
  }
  
  .date {
    font-size: 0.9em;
    color: #666;
    margin: 5px 0;
  }
  
  .buy-button {
    width: 100%;
    padding: 8px;
    margin-top: 10px;
    background-color: #2ecc71;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }
  
  .buy-button:hover:not(:disabled) {
    background-color: #27ae60;
  }
  
  .buy-button:disabled {
    background-color: #95a5a6;
    cursor: not-allowed;
  }
  
  .sold-out .card-image {
    opacity: 0.7;
  }
  
  .loading {
    text-align: center;
    padding: 40px;
  }
  
  .spinner {
    width: 50px;
    height: 50px;
    border: 5px solid #f3f3f3;
    border-top: 5px solid #3498db;
    border-radius: 50%;
    margin: 0 auto 20px;
    animation: spin 1s linear infinite;
  }
  
  @keyframes spin {
    0% { transform: rotate(0deg); }
    100% { transform: rotate(360deg); }
  }
  
  .error-message {
    text-align: center;
    color: #e74c3c;
    padding: 20px;
  }
  
  .filter-section {
    max-width: 900px;
    margin: 0 auto;
    padding: 0 20px;
  }
  
  .filter-container {
    background: white;
    border-radius: 12px;
    padding: 20px;
    box-shadow: 0 2px 15px rgba(0, 0, 0, 0.1);
  }
  
  .search-bar {
    position: relative;
    margin-bottom: 15px;
  }
  
  .search-icon {
    position: absolute;
    left: 15px;
    top: 50%;
    transform: translateY(-50%);
    color: #666;
    font-style: normal;
  }
  
  .search-bar input {
    width: 100%;
    padding: 12px 20px 12px 45px;
    border: 2px solid #eee;
    border-radius: 8px;
    font-size: 16px;
    transition: all 0.3s ease;
    background-color: #f8f9fa;
  }
  
  .search-bar input:focus {
    outline: none;
    border-color: #3498db;
    background-color: white;
    box-shadow: 0 0 0 3px rgba(52, 152, 219, 0.1);
  }
  
  .divider {
    height: 1px;
    background: #eee;
    margin: 15px 0;
  }
  
  .price-filter {
    display: flex;
    gap: 20px;
    align-items: flex-end;
  }
  
  .price-inputs {
    display: flex;
    align-items: flex-end;
    gap: 15px;
    flex-grow: 1;
  }
  
  .input-group {
    flex-grow: 1;
  }
  
  .input-group label {
    display: block;
    margin-bottom: 5px;
    color: #666;
    font-size: 0.9em;
  }
  
  .price-inputs input {
    width: 100%;
    padding: 10px 15px;
    border: 2px solid #eee;
    border-radius: 8px;
    font-size: 14px;
    background-color: #f8f9fa;
    transition: all 0.3s ease;
  }
  
  .price-inputs input:focus {
    outline: none;
    border-color: #3498db;
    background-color: white;
    box-shadow: 0 0 0 3px rgba(52, 152, 219, 0.1);
  }
  
  .price-separator {
    display: flex;
    align-items: center;
    color: #666;
    font-weight: bold;
    padding: 0 5px;
  }
  
  .filter-button {
    padding: 10px 25px;
    background-color: #3498db;
    color: white;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    transition: all 0.3s ease;
    font-size: 14px;
    font-weight: 500;
    height: 42px;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  
  .filter-button:hover {
    background-color: #2980b9;
    transform: translateY(-1px);
  }
  
  .filter-button:active {
    transform: translateY(0);
  }
  
  @media (max-width: 768px) {
    .price-filter {
      flex-direction: column;
      gap: 15px;
    }
    
    .price-inputs {
      flex-direction: column;
    }
    
    .price-separator {
      display: none;
    }
    
    .filter-button {
      width: 100%;
    }
  }
  
  .buy-button[disabled]:hover::after {
    content: '請先登入後再購買';
    position: absolute;
    bottom: 100%;
    left: 50%;
    transform: translateX(-50%);
    background: rgba(0, 0, 0, 0.8);
    color: white;
    padding: 5px 10px;
    border-radius: 4px;
    font-size: 12px;
    white-space: nowrap;
  }
  
  .success-message {
    position: fixed;
    top: 80px;
    left: 50%;
    transform: translateX(-50%);
    background-color: #2ecc71;
    color: white;
    padding: 15px 30px;
    border-radius: 4px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.2);
    z-index: 1000;
    animation: slideDown 0.3s ease-out;
  }
  
  @keyframes slideDown {
    from {
      transform: translate(-50%, -20px);
      opacity: 0;
    }
    to {
      transform: translate(-50%, 0);
      opacity: 1;
    }
  }
  </style> 