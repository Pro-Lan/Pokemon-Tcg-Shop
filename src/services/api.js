import axios from 'axios'

const api = axios.create({
    baseURL: 'http://localhost:5001/api',
    headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
    }
})

export const getCards = (params = {}) => {
    return api.get('/cards', { params })
}

export const getCardById = (id) => {
    return api.get(`/cards/${id}`)
}

export const createOrder = (order) => {
    return api.post('/orders', order)
}

export const getOrders = () => {
    return api.get('/orders')
}

export const registerUser = (user) => {
    return api.post('/users/register', user)
}

export const loginUser = (user) => {
    return api.post('/users/login', user)
} 