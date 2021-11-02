import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'http://localhost:5000/api/users/';

class UserService {
  getUserBoard() {
    return axios.get(API_URL + 'me', { headers: authHeader() });
  }
}

export default new UserService();
