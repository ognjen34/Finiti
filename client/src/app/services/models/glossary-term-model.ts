
export interface PaginationFilter {
    pageNumber: number;
    pageSize: number;
    termQuery?: string;
    authorQuery?: string;
  }
  
  export interface PaginationReturnObject<T> {
    items: T[];
    page: number;
    pageSize: number;
    totalItems: number;
  }
  
  export interface AuthorResponse {
    id: number;
    firstName: string;
    lastName: string;
    username: string;
    role : string;
  }
  
  export interface TermResponse {
    id: number;
    term: string;
    definition: string;
    createdAt: string;
    author: AuthorResponse;
    status: string; 
  }

  export interface CreateTermRequest {
    term: string;
    definition: string;
  }
export interface UpdateTermRequest {
    id: number;
    term: string;
    definition: string;
  }
  