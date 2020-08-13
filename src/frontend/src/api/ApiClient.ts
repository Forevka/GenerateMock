import axios, { AxiosResponse, AxiosInstance, AxiosRequestConfig } from 'axios';
import { IAuthModel } from '@/models/responses/IAuthModel';

class ApiClient {
    // tslint:disable-next-line: variable-name
    private _axiosClient: AxiosInstance;

    constructor() {
        this._axiosClient = axios.create({
            baseURL: 'http://194.99.21.140/api/mock',
        });
        this._axiosClient.interceptors.response.use((response) => this.authTokenExpiredInterceptor(response));
    }

    public async updateToken(): Promise<void> {
        this.fetchToken(localStorage.getItem('login'), localStorage.getItem('password'))
            .then((x) => {
                if (x.status === 200) {
                    localStorage.setItem('token', x.data.access_token);
                    this._axiosClient.defaults.headers.Authorization = x.data.access_token;
                }
            })
            .catch((x) => {
                // console.log(x);
            });
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


    public async get<T = any, R = AxiosResponse<T>>(url: string, config?: AxiosRequestConfig): Promise<R> {
        return this._axiosClient.get<T, R>(url, config);
    }

    public async post<T = any, R = AxiosResponse<T>>(url: string, data?: any, config?: AxiosRequestConfig): Promise<R> {
        return this._axiosClient.post<T, R>(url, data, config);
    }

    private authTokenExpiredInterceptor(response: AxiosResponse<any>): AxiosResponse<any> {
        if (response.status === 401) {
            // console.log('unathorized');
            this.fetchToken();
        }

        return response;
    }
}

export default new ApiClient();

