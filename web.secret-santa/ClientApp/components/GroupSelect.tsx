import * as React from 'react';
//import { RouteComponetProps } from 'react-router';
import 'isomorphic-fetch';


interface GroupsState {
    groups: Group[];
    loading: boolean;
}

export class GroupSelect extends React.Component<{}, GroupsState> {
    constructor() {
        super();
        this.state = { groups: [], loading: true };

        fetch('api/SampleData/Groups')
            .then(response => response.json() as Promise<Group[]>)
            .then(data => {
                this.setState({ groups: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
                ? <option>Loading...</option>
                : GroupSelect.renderGroupOptions(this.state.groups);

            return <div>
                <select>
                    {contents}
                </select>

            </div>;
    }
    
    private static renderGroupOptions(groups: Group[]) {
        return  groups.map(group => <option key={group.id} value={group.id}>{group.name}</option>);
    }
}

interface Group {
    name: string;
    id: number;
}
