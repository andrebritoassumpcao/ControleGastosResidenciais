export interface PersonRequestDto {
  name: string;
  age?: number;
}

export interface PersonResponseDto {
  id: string;
  name: string;
  age: number;
}
