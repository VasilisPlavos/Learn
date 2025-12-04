export default class HttpException extends Error {
    constructor(statusCode: number, message: string, error?: string) {
        super(message);

        this.statusCode = statusCode;
        this.message = message;
        this.error = error || null;
    }

    statusCode?: number;
    status?: number;
    message: string;
    error: string | null;
}