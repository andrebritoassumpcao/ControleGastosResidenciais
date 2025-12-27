export interface SummaryByPersonDto {
  personId: string;
  name: string;
  totalIncome: number;
  totalExpenses: number;
  balance: number;
}

export interface SummaryByPersonResponseDto {
  items: SummaryByPersonDto[];
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface SummaryByCategoryDto {
  categoryId: string;
  description: string;
  totalIncome: number;
  totalExpenses: number;
  balance: number;
}

export interface SummaryByCategoryResponseDto {
  items: SummaryByCategoryDto[];
  totalIncome: number;
  totalExpense: number;
  balance: number;
}
