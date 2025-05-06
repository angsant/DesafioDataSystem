import React, { useState, useEffect } from 'react'
//import reactLogo from './assets/react.svg'
//import viteLogo from '/vite.svg'
import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader, Input } from 'reactstrap'
import logoCadastro from './assets/cadastro.png';

function App() {
    const baseUrl = "https://localhost:7122/Tarefa";

    const [data, setData] = useState([]);
    const [updateData, setUpdateData] = useState(true);

    const [modalIncluir, setModalIncluir] = useState(false);
    const [modalEditar, setModalEditar] = useState(false);
    const [modalExcluir, setModalExcluir] = useState(false);

    const [searchTerm, setSearchTerm] = useState('');

    const [tarefaSelecionada, setTarefaSelecionada] = useState({
        id: '',
        titulo: '',
        descricao: '',
        dataConclusao: null,
        status: '1'
    })

    const selecionarTarefa = (tarefa, opcao) => {
        setTarefaSelecionada(tarefa);
        (opcao === "Editar") ?
            abrirFecharModalEditar() : abrirFecharModalExcluir();
    }

    const esvaziarTarefa = () => {
        setTarefaSelecionada({
            id: '',
            titulo: '',
            descricao: '',
            dataConclusao: null,
            status: '1'
        });
    }

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const filteredData = data.sort((a, b) => a.id - b.id).filter(item =>
        String(item.status).toLowerCase().includes(searchTerm.toLowerCase())
    );

    const statusDescriptions = {
        1: 'Pendente',
        2: 'Em Progresso',
        3: 'Concluída',
    };


    const abrirFecharModalIncluir = () => {
        setModalIncluir(!modalIncluir);
    }

    const abrirFecharModalEditar = () => {
        setModalEditar(!modalEditar);
    }

    const abrirFecharModalExcluir = () => {
        setModalExcluir(!modalExcluir);
    }

    const handleChange = e => {
        const { name, value } = e.target;
        setTarefaSelecionada({
            ...tarefaSelecionada, [name]: value
        });
        console.log(tarefaSelecionada);
    }

    const pedidoGet = async () => {
        await axios.get(baseUrl)
            .then(response => {
                setData(response.data);
            }).catch(error => {
                console.log(error);
            })
    }

    const pedidoPost = async () => {
        delete tarefaSelecionada.id;
        tarefaSelecionada.status = parseInt(tarefaSelecionada.status);
        await axios.post(baseUrl, tarefaSelecionada)
            .then(response => {
                setData(data.concat(response.data));
                setUpdateData(true);
                abrirFecharModalIncluir();
                esvaziarTarefa();
            }).catch(error => {
                console.log(error);
            })
    }

    const pedidoPut = async () => {
        tarefaSelecionada.status = parseInt(tarefaSelecionada.status);
        await axios.put(baseUrl + "/" + tarefaSelecionada.id, tarefaSelecionada)
            .then(response => {
                var resposta = response.data;
                var dadosAuxiliar = data;
                dadosAuxiliar.map(tarefa => {
                    if (tarefa.id === tarefaSelecionada.id) {
                        tarefa.titulo = resposta.titulo;
                        tarefa.descricao = resposta.descricao;
                        tarefa.dataConclusao = resposta.dataConclusao;
                        tarefa.status = resposta.status;
                    }
                });
                setUpdateData(true);
                abrirFecharModalEditar();
                esvaziarTarefa();
            }).catch(error => {
                console.log(error);
            })
    }

    const pedidoDelete = async () => {
        await axios.delete(baseUrl + "/" + tarefaSelecionada.id, {
            data: {
                id: parseInt(tarefaSelecionada.id)
            }
        })
            .then(response => {
                setData(data.filter(tarefa => tarefa.id !== response.data));
                setUpdateData(true);
                abrirFecharModalExcluir();
                esvaziarTarefa();
            }).catch(error => {
                console.log(error);
            })
    }

    useEffect(() => {
        if (updateData) {
            pedidoGet();
            setUpdateData(false);
        }
    }, [updateData])

    return (
        <>
            <div className="tarefa-container">
                <br />
                <h3>Cadastro de Tarefas</h3>
                <header>
                    <img src={logoCadastro} alt='Cadastro' />
                    <button className="btn btn-success" onClick={() => abrirFecharModalIncluir()}>Incluir Nova Tarefa</button>
                </header>
                <label>Filtro de Status: </label>
                <br />
                <Input type="select" className="form-control" name="status" onChange={handleSearchChange}
                    value={searchTerm}>
                    <option value="">Todas</option>
                    <option value="1">Pendente</option>
                    <option value="2">Em Progresso</option>
                    <option value="3">Concluída</option>
                </Input>
                <br />
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Título</th>
                            <th>Descrição</th>
                            <th>Data de Criação</th>
                            <th>Data de Conclusão</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredData.map(tarefa => (
                            <tr key={tarefa.id}>
                                <td>{tarefa.id}</td>
                                <td>{tarefa.titulo}</td>
                                <td>{tarefa.descricao}</td>
                                <td>{tarefa.dataCriacao}</td>
                                <td>{tarefa.dataConclusao}</td>
                                <td>{statusDescriptions[tarefa.status]}</td>
                                <td>
                                    <button className="btn btn-primary" onClick={() => selecionarTarefa(tarefa, "Editar")}>Editar</button> {"  "}
                                    <button className="btn btn-danger" onClick={() => selecionarTarefa(tarefa, "Excluir")}>Excluir</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                <Modal isOpen={modalIncluir}>
                    <ModalHeader>Incluir Tarefa</ModalHeader>
                    <ModalBody>
                        <div className="form-group">
                            <label>Título: </label>
                            <br />
                            <input type="text" className="form-control" name="titulo" onChange={handleChange} />
                            <br />
                            <label>Descrição: </label>
                            <br />
                            <input type="text" className="form-control" name="descricao" onChange={handleChange} />
                            <br />
                            <label>Data de Conclusão: </label>
                            <br />
                            <input type="date" className="form-control" name="dataConclusao" onChange={handleChange} />
                            <br />
                            <label>Status: </label>
                            <br />
                            <Input type="select" className="form-control" name="status" onChange={handleChange}>
                                <option value="1">Pendente</option>
                                <option value="2">Em Progresso</option>
                                <option value="3">Concluída</option>
                            </Input>
                            <br />
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <button className="btn btn-primary" onClick={() => pedidoPost()}>Incluir</button>
                        <button className="btn btn-danger" onClick={() => abrirFecharModalIncluir()} >Cancelar</button>
                    </ModalFooter>
                </Modal>


                <Modal isOpen={modalEditar}>
                    <ModalHeader>Editar Tarefa</ModalHeader>
                    <ModalBody>
                        <div className="form-group">
                            <label>ID: </label>
                            <input type="text" className="form-control" readOnly
                                value={tarefaSelecionada && tarefaSelecionada.id} />
                            <br />
                            <label>Título: </label><br />
                            <input type="text" className="form-control" name="titulo" onChange={handleChange}
                                value={tarefaSelecionada && tarefaSelecionada.titulo} /><br />
                            <label>Descrição: </label><br />
                            <input type="text" className="form-control" name="descricao" onChange={handleChange}
                                value={tarefaSelecionada && tarefaSelecionada.descricao} /><br />
                            <label>Data de Conclusão: </label><br />
                            <input type="date" className="form-control" name="dataConclusao" onChange={handleChange}
                                value={tarefaSelecionada && tarefaSelecionada.dataConclusao} /><br />
                            <label>Status: </label><br />
                            <Input type="select" className="form-control" name="status" onChange={handleChange}
                                value={tarefaSelecionada && tarefaSelecionada.status}>
                                <option value="1">Pendente</option>
                                <option value="2">Em Progresso</option>
                                <option value="3">Concluída</option>
                            </Input>

                            <br />
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <button className="btn btn-primary" onClick={() => pedidoPut()}>Editar</button>{"  "}
                        <button className="btn btn-danger" onClick={() => abrirFecharModalEditar()} >Cancelar</button>
                    </ModalFooter>
                </Modal>

                <Modal isOpen={modalExcluir}>
                    <ModalBody>
                        Tem certeza de que deseja excluir a tarefa {tarefaSelecionada && tarefaSelecionada.titulo} ?
                    </ModalBody>
                    <ModalFooter>
                        <button className="btn btn-danger" onClick={() => pedidoDelete()} > Sim </button>
                        <button className="btn btn-secondary" onClick={() => abrirFecharModalExcluir()}> Não </button>
                    </ModalFooter>
                </Modal>

            </div>
        </>
    )
}

export default App
