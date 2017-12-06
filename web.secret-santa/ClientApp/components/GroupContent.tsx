import * as React from 'react';
//import { RouteComponetProps } from 'react-router';
import 'isomorphic-fetch';


interface GroupContentState {
    users: SecretSantaUser[];
    loading: boolean;
}

export class GroupContent extends React.Component<{}, GroupContentState> {
    constructor() {
        super();
        this.state = { users: [], loading: true };

        fetch('api/SampleData/Users/1')
            .then(response => response.json() as Promise<SecretSantaUser[]>)
            .then(data => {
                this.setState({ users: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p>Loading...</p>
            : GroupContent.renderGroupContent(this.state.users);

        return <div>
            {contents}
        </div>;
    }

    private static renderGroupContent(users: SecretSantaUser[]) {
        return <ul>
            {users.map(user => <li key={user.id}>{user.name} - {user.email}</li>)}
        </ul>;
    }
}


interface SecretSantaUser {
    id: number;
    groupid: number;
    name: string;
    email: string;
    letter: string;
}