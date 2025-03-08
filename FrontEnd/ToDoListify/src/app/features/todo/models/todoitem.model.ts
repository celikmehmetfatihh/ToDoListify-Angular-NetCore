export interface ToDoItem {
    id: string;
    createDate: string;
    title: string;
    isCompleted: boolean;
    detail?: string;
    userId: string;
    priorityId?: string;
}