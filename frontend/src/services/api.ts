import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7032/api', 
});

export default api;
