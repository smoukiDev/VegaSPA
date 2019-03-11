export interface QueryResult<T> {
    totalItems: number;
    items: Array<T>;
}