export interface UpdateToDoRequest {
    title: string;
    isCompleted: boolean;
    detail?: string;
    priorityId?: string | null;
    userId: string;
}