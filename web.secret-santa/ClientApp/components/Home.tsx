import * as React from 'react';
import { GroupSelect } from './GroupSelect';
import { GroupContent } from './GroupContent';

export class Home extends React.Component {
    public render() {
        return <div className='row'>
            <div>
                <GroupSelect />
            </div>

            <div>
                <GroupContent />
            </div>
        </div>;
    }
}
