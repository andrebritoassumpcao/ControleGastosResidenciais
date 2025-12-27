export type CategoryPurpose = 0 | 1 | 2;
// 0: Expenses, 1: Income, 2: Both
export interface CategoryRequestDto {
  description: string;
  purpose: CategoryPurpose;
}

export interface CategoryResponseDto {
  id: string;
  description: string;
  purpose: CategoryPurpose;
}
