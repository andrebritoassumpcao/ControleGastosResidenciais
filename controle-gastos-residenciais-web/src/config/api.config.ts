export const API_CONFIG = {
  baseURL: import.meta.env.VITE_API_URL,
  timeout: 60000,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
  withCredentials: false,
} as const;
