import { AxiosAPI } from "@/api/Axios";
import type {
  SummaryByPersonResponseDto,
  SummaryByPersonDto,
  SummaryByCategoryResponseDto,
  SummaryByCategoryDto,
} from "@/types/summary/Summary";

export class SummaryService {
  private static readonly endpoint = "/api/v1/summary";

  static async getAllPersonSummary(): Promise<SummaryByPersonResponseDto> {
    try {
      const response = await AxiosAPI.get<SummaryByPersonResponseDto>(
        `${this.endpoint}/person-summary`
      );
      console.log("Response data:", response.data);
      return response.data;
    } catch (error) {
      console.error(
        "SummaryService: Erro ao buscar relatório de todas as pessoas:",
        error
      );
      throw error;
    }
  }

  static async getPersonSummaryById(
    personId: string
  ): Promise<SummaryByPersonDto> {
    try {
      const response = await AxiosAPI.get<SummaryByPersonDto>(
        `${this.endpoint}/person-summary/${personId}`
      );
      return response.data;
    } catch (error) {
      console.error(
        `SummaryService: Erro ao buscar relatório da pessoa ${personId}:`,
        error
      );
      throw error;
    }
  }

  static async getAllCategorySummary(): Promise<SummaryByCategoryResponseDto> {
    try {
      const response = await AxiosAPI.get<SummaryByCategoryResponseDto>(
        `${this.endpoint}/category-summary`
      );
      return response.data;
    } catch (error) {
      console.error(
        "SummaryService: Erro ao buscar relatório de todas as categorias:",
        error
      );
      throw error;
    }
  }

  static async getCategorySummaryById(
    categoryId: string
  ): Promise<SummaryByCategoryDto> {
    try {
      const response = await AxiosAPI.get<SummaryByCategoryDto>(
        `${this.endpoint}/category-summary/${categoryId}`
      );
      return response.data;
    } catch (error) {
      console.error(
        `SummaryService: Erro ao buscar relatório da categoria ${categoryId}:`,
        error
      );
      throw error;
    }
  }
}

export default SummaryService;
