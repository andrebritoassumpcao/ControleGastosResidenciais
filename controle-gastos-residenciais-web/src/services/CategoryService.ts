// src/services/CategoryService.ts
import type {
  CategoryRequestDto,
  CategoryResponseDto,
} from "@/types/category/Category";
import { AxiosAPI } from "@/api/Axios";

class CategoryService {
  private static readonly endpoint = "/api/v1/category";

  static async getAll(): Promise<CategoryResponseDto[]> {
    try {
      const response = await AxiosAPI.get<CategoryResponseDto[]>(this.endpoint);
      return response.data;
    } catch (error) {
      console.error("CategoryService: Erro ao buscar categorias:", error);
      throw error;
    }
  }

  static async getById(id: string): Promise<CategoryResponseDto> {
    try {
      const response = await AxiosAPI.get<CategoryResponseDto>(
        `${this.endpoint}/${id}`
      );
      return response.data;
    } catch (error) {
      console.error(
        `CategoryService: Erro ao buscar categoria pelo id: ${id}:`,
        error
      );
      throw error;
    }
  }

  static async createCategory(
    data: CategoryRequestDto
  ): Promise<CategoryResponseDto> {
    try {
      const response = await AxiosAPI.post<CategoryResponseDto>(
        this.endpoint,
        data
      );
      return response.data;
    } catch (error) {
      console.error("CategoryService: Erro ao criar categoria:", error);
      throw error;
    }
  }

  static async deleteCategory(id: string): Promise<void> {
    try {
      await AxiosAPI.delete(`${this.endpoint}/${id}`);
    } catch (error) {
      console.error(`CategoryService: Erro ao deletar categoria ${id}:`, error);
      throw error;
    }
  }
}

export default CategoryService;
