import { AxiosAPI } from "@/api/Axios";
import type {
  TransactionRequestDto,
  TransactionResponseDto,
} from "@/types/transaction/Transaction";

class TransactionService {
  private static readonly endpoint = "/api/v1/transaction";

  static async getAll(): Promise<TransactionResponseDto[]> {
    try {
      const response = await AxiosAPI.get<TransactionResponseDto[]>(
        this.endpoint
      );
      return response.data;
    } catch (error) {
      console.error("TransactionService: Erro ao buscar transações:", error);
      throw error;
    }
  }

  static async getById(id: string): Promise<TransactionResponseDto> {
    try {
      const response = await AxiosAPI.get<TransactionResponseDto>(
        `${this.endpoint}/${id}`
      );
      return response.data;
    } catch (error) {
      console.error(
        `TransactionService: Erro ao buscar transação pelo id: ${id}:`,
        error
      );
      throw error;
    }
  }

  static async createTransaction(
    data: TransactionRequestDto
  ): Promise<TransactionResponseDto> {
    try {
      const response = await AxiosAPI.post<TransactionResponseDto>(
        this.endpoint,
        data
      );
      return response.data;
    } catch (error) {
      console.error("TransactionService: Erro ao criar transação:", error);
      throw error;
    }
  }
}

export default TransactionService;
