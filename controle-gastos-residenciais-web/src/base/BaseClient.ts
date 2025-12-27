import axios from "axios";
import type { AxiosInstance, AxiosResponse, AxiosError } from "axios";
import { API_CONFIG } from "@/config/api.config";

export class BaseClient {
  protected client: AxiosInstance;

  constructor() {
    this.client = axios.create(API_CONFIG);
    this.setupInterceptors();
  }

  private setupInterceptors(): void {
    this.client.interceptors.request.use(
      (config) => {
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    this.client.interceptors.response.use(
      (response: AxiosResponse) => {
        return response;
      },
      (error: AxiosError) => {
        return Promise.reject(error);
      }
    );
  }
}
