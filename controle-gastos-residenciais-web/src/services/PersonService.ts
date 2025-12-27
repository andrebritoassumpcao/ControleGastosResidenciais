import type {
  PersonRequestDto,
  PersonResponseDto,
} from "@/types/person/Person";
import { AxiosAPI } from "@/api/Axios";

class PersonService {
  private static readonly endpoint = "/api/v1/person";

  static async getAll(): Promise<PersonResponseDto[]> {
    try {
      const response = await AxiosAPI.get<PersonResponseDto[]>(this.endpoint);
      return response.data;
    } catch (error) {
      console.error("PersonService: Erro ao buscar pessoas:", error);
      throw error;
    }
  }

  static async getById(id: string): Promise<PersonResponseDto> {
    try {
      const response = await AxiosAPI.get<PersonResponseDto>(
        `${this.endpoint}/${id}`
      );
      return response.data;
    } catch (error) {
      console.error(
        `PersonService: Erro ao buscar pessoa pelo id: ${id}:`,
        error
      );
      throw error;
    }
  }

  static async createPerson(
    data: PersonRequestDto
  ): Promise<PersonResponseDto> {
    try {
      const response = await AxiosAPI.post<PersonResponseDto>(
        this.endpoint,
        data
      );
      return response.data;
    } catch (error) {
      console.error("PersonService: Erro ao criar pessoa:", error);
      throw error;
    }
  }

  static async deletePerson(id: string): Promise<void> {
    try {
      await AxiosAPI.delete(`${this.endpoint}/${id}`);
    } catch (error) {
      console.error(`PersonService: Erro ao deletar pessoa ${id}:`, error);
      throw error;
    }
  }
}

export default PersonService;
