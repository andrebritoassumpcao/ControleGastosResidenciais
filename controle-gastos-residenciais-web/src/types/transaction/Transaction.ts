export type TransactionType = 0 | 1;

export interface TransactionRequestDto {
  description: string;
  value: number;
  type: TransactionType;
  personId: string;
  categoryId: string;
}

export interface TransactionResponseDto {
  id: string;
  description: string;
  value: number;
  type: TransactionType;
  personId: string;
  personName: string;
  categoryId: string;
}
