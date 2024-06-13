import axios from "axios";

const request = axios.create({
  baseURL: "https://localhost:7289"
});

export default request;