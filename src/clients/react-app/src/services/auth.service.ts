import axios from "axios";

const API_URL = "http://localhost:5000/api/auth/";

class AuthService {
  login(username: string, password: string) {
    return axios
      .post(API_URL + "login", {
        username,
        password
      })
      .then(response => {
        if (response.data.token) {
          localStorage.setItem("token", JSON.stringify(response.data.token));
        }

        if (response.data.user) {
          localStorage.setItem("user", JSON.stringify(response.data.user));
        }

        return response.data.user;
      });
  }

  getCurrentUser() {
    const userStr = localStorage.getItem("user");
    if (userStr) return JSON.parse(userStr);

    return null;
  }

  logout() {
    localStorage.removeItem("user");
    localStorage.removeItem("token");
  }
}

export default new AuthService();
