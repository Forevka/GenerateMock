import axios, { AxiosResponse, AxiosInstance, AxiosRequestConfig, AxiosError } from 'axios';
import { IAuthModel } from '../models/responses/IAuthModel';
import { IGetMe } from '../models/responses/IGetMe';
import router from '../router';

class ApiClient {
    public User: any = {};
    public IsLogged: boolean = false;

    // tslint:disable-next-line: variable-name
    private _axiosClient: AxiosInstance;

    constructor() {
        this._axiosClient = axios.create({
            baseURL: 'http://194.99.21.140/api/mock',
        });
        this._axiosClient.interceptors.response.use((response) => this.ok(response),
        (error) => this.authTokenExpiredInterceptor(error));
    }

    public updateToken(token: string): void {
        localStorage.setItem('token', token);
        this._axiosClient.defaults.headers.Authorization = token;
        this.IsLogged = true;
    }

    public async renewToken(): Promise<void> {
        const token = localStorage.getItem('token');
        this._axiosClient.defaults.headers.Authorization = token;
        const resp = await this.getMe();
        if (resp) {
            this.IsLogged = true;
            this.User = resp.data;
            return;
        }
        this.IsLogged = false;
        this.User = undefined;
    }

    public async fetchToken(login: string|null = '', password: string|null = ''):
        Promise<AxiosResponse<IAuthModel>> {
        return await this._axiosClient.get<IAuthModel>('Authorization', {
            params: {
                login: login ? login : 'anonim',
                password: password ? password : 'WERDwerd2012',
            },
        });
    }

    public async registerUser(login: string, password: string):
        Promise<AxiosResponse<IAuthModel>> {
        return await this._axiosClient.post<IAuthModel>(`Registration?login=${login}&password=${password}`);
    }

    public async getMe(): Promise<AxiosResponse<IGetMe>> {
        return await this._axiosClient.get('Authorization/Me');
    }


    public async get<T = any, R = AxiosResponse<T>>(url: string, config?: AxiosRequestConfig): Promise<R> {
        return this._axiosClient.get<T, R>(url, config);
    }

    public async post<T = any, R = AxiosResponse<T>>(url: string, data?: any, config?: AxiosRequestConfig): Promise<R> {
        return this._axiosClient.post<T, R>(url, data, config);
    }

    private ok(response: AxiosResponse<any>): AxiosResponse<any> {
        // this.IsLogged = true;
        return response;
    }

    private authTokenExpiredInterceptor(error: AxiosError): void {
        if (error.response && error.response.status === 401) {
            this.IsLogged = false;
            localStorage.removeItem('token');
            // router.push('/login');
        }
    }
}

export default new ApiClient();

